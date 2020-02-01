using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileViewer
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// 解凍先のディレクトリ
        /// </summary>
        private string _decompressedDirectoryPath;

        /// <summary>
        /// 解凍制御クラスのインスタンス
        /// </summary>
        private Decompressor.IDecompressor _decompressor = new Decompressor.ZipDecompressor();

        /// <summary>
        /// 
        /// </summary>
        class ComboBoxItem
        {
            private string Text { get; set; }
            public View View { get; private set; }

            public ComboBoxItem(string text, View view)
            {
                Text = text;
                View = view;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path"></param>
        public FormMain(string path)
        {
            InitializeComponent();

            InitializeViews();

            DecompressAndOpen(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool DecompressAndOpen(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }

            if (Path.GetExtension(path) != _decompressor.Extension)
            {
                return false;
            }

            using (var worker = new WorkerProgressForm())
            {
                FileManager.DeleteTempDirectory(_decompressedDirectoryPath);

                _decompressedDirectoryPath = FileManager.GetTempDirectory();
                
                var param = new WorkerProgressForm.WorkerParameter();
                param.Decompressor = _decompressor;
                param.InputFilePath = path;
                param.OutputDirectoryPath = _decompressedDirectoryPath;
                worker.Start(param);

                if (worker.ShowDialog(this) == DialogResult.OK)
                {
                    var result = worker.Result;

                    imageListLargeIcon.Images.AddRange(result.LargeIconList.ToArray());
                    imageListSmallIcon.Images.AddRange(result.SmallIconList.ToArray());
                    treeViewFile.Nodes.Clear();
                    treeViewFile.Nodes.Add(result.RootNode.FirstNode);

                    SetCurrentNode(treeViewFile.Nodes);

                    treeViewFile.ExpandAll();
                }
            }
            return false;
        }

        /// <summary>
        /// ノード指定してリストビューを作成する
        /// </summary>
        /// <param name="nodes"></param>
        private void SetCurrentNode(TreeNodeCollection nodes)
        {
            listViewFile.Items.Clear();

            foreach (TreeNode node in nodes)
            {
                var fileInfo = node.Tag as FileManager.Info;

                ListViewItem item;
                if (Directory.Exists(fileInfo.FilePath))
                {
                    item = new ListViewItem(node.Text);
                }
                else
                {
                    item = new ListViewItem(new string[] {
                        node.Text,
                        FileManager.BytesToString(fileInfo.FileType.Length),
                        fileInfo.FileType,
                        fileInfo.FileInfo.LastWriteTime.ToString()
                    });
                }
                item.ImageIndex = node.ImageIndex;
                item.Tag = node;
                listViewFile.Items.Add(item);
            }
        }

        /// <summary>
        /// 選択中の最初のノードを取得する
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private TreeNode GetSelectedNode(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.IsSelected)
                {
                    return node;
                }

                var selected = GetSelectedNode(node.Nodes);

                if (selected != null)
                {
                    return selected;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeViews()
        {
            toolStripComboBoxViewStyle.Items.Add(new ComboBoxItem("大アイコン", View.LargeIcon));
            toolStripComboBoxViewStyle.Items.Add(new ComboBoxItem("小アイコン", View.SmallIcon));
            toolStripComboBoxViewStyle.Items.Add(new ComboBoxItem("リスト表示", View.List));
            toolStripComboBoxViewStyle.Items.Add(new ComboBoxItem("詳細表示", View.Details));
            toolStripComboBoxViewStyle.Items.Add(new ComboBoxItem("タイル", View.Tile));

            // ToolStripMenu の「編集」をそのままコンテキストメニューに
            var contextMenuStrip = new ContextMenuStrip();
            var toolStripMenus = new List<ToolStripMenuItem>();
            foreach (var toolStripMenuItem in toolStripMenuItemEdit.DropDownItems)
            {
                if (toolStripMenuItem is ToolStripMenuItem)
                {
                    var dstItem = toolStripMenuItem as ToolStripMenuItem;
                    if (!dstItem.Available) continue;

                    var newItem = new ToolStripMenuItem(dstItem.Text, dstItem.Image);

                    // ハック的なイベントのコピー
                    // https://stackoverflow.com/questions/6055038/how-to-clone-control-event-handlers-at-run-time
                    var eventsField = typeof(Component).GetField("events",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    var eventHandlerList = eventsField.GetValue(dstItem);
                    eventsField.SetValue(newItem, eventHandlerList);

                    toolStripMenus.Add(newItem);
                }
            }
            contextMenuStrip.Items.AddRange(toolStripMenus.ToArray());

            listViewFile.ContextMenuStrip = contextMenuStrip;
            treeViewFile.ContextMenuStrip = contextMenuStrip;

            var columnFileName = new ColumnHeader();
            columnFileName.Text = "名前";
            columnFileName.Width = 180;
            var columnFileSize = new ColumnHeader();
            columnFileSize.Text = "サイズ";
            columnFileSize.Width = 70;
            var columnFileType = new ColumnHeader();
            columnFileType.Text = "ファイルタイプ";
            columnFileType.Width = 90;
            var columnLastWriteTime = new ColumnHeader();
            columnLastWriteTime.Text = "更新日時";
            columnLastWriteTime.Width = 120;
            ColumnHeader[] colHeaderRegValue = { columnFileName, columnFileSize, columnFileType, columnLastWriteTime };
            listViewFile.Columns.AddRange(colHeaderRegValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.WindowSize.Width > 0 && Properties.Settings.Default.WindowSize.Height > 0)
            {
                Size = Properties.Settings.Default.WindowSize;
            }
            splitContainerMain.SplitterDistance = Properties.Settings.Default.SplitterDistance;
            toolStripComboBoxViewStyle.SelectedIndex = Properties.Settings.Default.ViewStyleIndex;

            ComboBoxItem item = toolStripComboBoxViewStyle.Items[toolStripComboBoxViewStyle.SelectedIndex] as ComboBoxItem;
            listViewFile.View = item.View;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileManager.DeleteTempDirectory(_decompressedDirectoryPath);

            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.WindowSize = Size;
            }
            Properties.Settings.Default.SplitterDistance = splitContainerMain.SplitterDistance;
            Properties.Settings.Default.ViewStyleIndex = toolStripComboBoxViewStyle.SelectedIndex;

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewFile_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = GetSelectedNode((sender as TreeView).Nodes);

            if (node != null)
            {
                var fileInfo = node.Tag as FileManager.Info;

                SetCurrentNode(Directory.Exists(fileInfo.FilePath) ? node.Nodes : node.Parent.Nodes);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var clickedItem = (sender as ListView).HitTest(e.Location).Item;

            if (clickedItem != null)
            {
                var treeNode = clickedItem.Tag as TreeNode;
                var fileInfo = treeNode.Tag as FileManager.Info;

                if (Directory.Exists(fileInfo.FilePath))
                {
                    SetCurrentNode(treeNode.Nodes);
                }
                else
                {
                    // 関連付けがない場合、例外が投げられる
                    // その場合は単に無視する
                    try { System.Diagnostics.Process.Start(fileInfo.FilePath); } catch {}
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var clickedItem = (sender as TreeView).HitTest(e.Location).Node;

            if (clickedItem != null)
            {
                var fileInfo = clickedItem.Tag as FileManager.Info;

                if (!Directory.Exists(fileInfo.FilePath))
                {
                    try { System.Diagnostics.Process.Start(fileInfo.FilePath); } catch {}
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemFileOpen_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_decompressor.Name);
            sb.Append("|");
            sb.Append("*");
            sb.Append(_decompressor.Extension);

            openFileDialog.Filter = sb.ToString();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DecompressAndOpen(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemEditCopy_Click(object sender, EventArgs e)
        {
            var files = new System.Collections.Specialized.StringCollection();

            foreach (ListViewItem item in listViewFile.Items)
            {
                if (item.Selected)
                {
                    var node = item.Tag as TreeNode;
                    var fileInfo = node.Tag as FileManager.Info;
                    files.Add(fileInfo.FilePath);
                }
            }

            if (files.Count > 0)
            {
                Clipboard.SetFileDropList(files);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemEditSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewFile.Items)
            {
                item.Selected = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemToolOption_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemHelpVersionInfo_Click(object sender, EventArgs e)
        {
            using (var form = new Form())
            {
                form.SuspendLayout();
                form.StartPosition = FormStartPosition.CenterParent;
                form.AutoScaleDimensions = new SizeF(6F, 12F);
                form.AutoScaleMode = AutoScaleMode.Font;
                form.ClientSize = new Size(230, 60);
                form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                form.Text = "バージョン情報";

                var iconPictureBox = new PictureBox();
                iconPictureBox.Image = Icon.ToBitmap();
                iconPictureBox.Width = Icon.Width;
                iconPictureBox.Height = Icon.Height;
                iconPictureBox.Left = 20;
                iconPictureBox.Top = 15;
                form.Controls.Add(iconPictureBox);

                var fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(
                    System.Reflection.Assembly.GetExecutingAssembly().Location);

                var versionLabel = new Label();
                versionLabel.Text = fileVersionInfo.ProductName + " " + fileVersionInfo.FileVersion.ToString();
                versionLabel.AutoSize = true;
                versionLabel.Left = iconPictureBox.Left + iconPictureBox.Width + 15;
                versionLabel.Top = iconPictureBox.Top + (iconPictureBox.Height / 2) - 15;
                form.Controls.Add(versionLabel);

                var copyrightLabel = new Label();
                copyrightLabel.Text = fileVersionInfo.LegalCopyright;
                copyrightLabel.AutoSize = true;
                copyrightLabel.Left = versionLabel.Left;
                copyrightLabel.Top = versionLabel.Top + versionLabel.Height + 5;
                form.Controls.Add(copyrightLabel);

                form.ResumeLayout(false);
                form.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainerMain_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Properties.Settings.Default.SplitterDistance = (sender as SplitContainer).SplitterDistance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBoxViewStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBoxViewStyle.Selected)
            {
                ComboBoxItem item = toolStripComboBoxViewStyle.Items[toolStripComboBoxViewStyle.SelectedIndex] as ComboBoxItem;
                listViewFile.View = item.View;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewFile_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var listView = sender as ListView;
            var paths = new List<string>();

            foreach (var item in listView.SelectedItems.Cast<ListViewItem>())
            {
                var node = item.Tag as TreeNode;
                var fileInfo = node.Tag as FileManager.Info;
                paths.Add(fileInfo.FilePath);
            }

            DataObject dataObj = new DataObject(DataFormats.FileDrop, paths.ToArray());
            DragDropEffects effect = DragDropEffects.Copy;
            listViewFile.DoDragDrop(dataObj, effect);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewFile_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var node = e.Item as TreeNode;
            var fileInfo = node.Tag as FileManager.Info;

            string[] paths = { fileInfo.FilePath };
            DataObject dataObj = new DataObject(DataFormats.FileDrop, paths);
            DragDropEffects effect = DragDropEffects.Copy | DragDropEffects.Move;
            treeViewFile.DoDragDrop(dataObj, effect);
        }
    }
}
