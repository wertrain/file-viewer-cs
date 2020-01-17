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

        public FormMain()
        {
            InitializeComponent();

            Open(@"D:\Develop\C#\file-viewer-cs\FileViewer\FileViewer");
        }

        private bool Open(string path)
        {
            listViewFile.View = View.LargeIcon;

            CreateNode(path, treeViewFile.Nodes);
            SetCurrentNode(treeViewFile.Nodes);

            treeViewFile.ExpandAll();

            return true;
        }

        private void SetCurrentNode(TreeNodeCollection nodes)
        {
            listViewFile.Items.Clear();

            foreach (TreeNode node in nodes)
            {
                var fullPath = (string)node.Tag;

                ListViewItem item;
                if (Directory.Exists(fullPath))
                {
                    item = new ListViewItem(node.Text);
                }
                else
                {
                    FileInfo info = new FileInfo(fullPath);
                    item = new ListViewItem(new string[] { node.Text, FileManager.BytesToString(info.Length), info.LastWriteTime.ToString() });
                }
                item.ImageIndex = node.ImageIndex;
                item.Tag = node;
                listViewFile.Items.Add(item);
            }
        }

        private void CreateNode(string path, TreeNodeCollection nodes)
        {
            var index = imageListLargeIcon.Images.Count;
            var licon = FileManager.IconUtility.GetIconImage(path, true);
            var sicon = FileManager.IconUtility.GetIconImage(path, false);
            imageListLargeIcon.Images.Add(licon);
            imageListSmallIcon.Images.Add(sicon);

            var node = new TreeNode(Path.GetFileName(path));
            node.ImageIndex = node.SelectedImageIndex = index;
            node.Tag = path;
            nodes.Add(node);

            if (Directory.Exists(path))
            {
                foreach (var entry in Directory.GetDirectories(path, "*"))
                {
                    CreateNode(entry, node.Nodes);
                }

                foreach (var entry in Directory.GetFiles(path, "*"))
                {
                    CreateNode(entry, node.Nodes);
                }
            }
        }

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

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.WindowSize = Size;
            }
            Properties.Settings.Default.SplitterDistance = splitContainerMain.SplitterDistance;
            Properties.Settings.Default.ViewStyleIndex = toolStripComboBoxViewStyle.SelectedIndex;

            Properties.Settings.Default.Save();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            toolStripComboBoxViewStyle.Items.Add(new ComboBoxItem("大アイコン", View.LargeIcon));
            toolStripComboBoxViewStyle.Items.Add(new ComboBoxItem("小アイコン", View.SmallIcon));
            toolStripComboBoxViewStyle.Items.Add(new ComboBoxItem("リスト表示", View.List));
            toolStripComboBoxViewStyle.Items.Add(new ComboBoxItem("詳細表示", View.Details));
            toolStripComboBoxViewStyle.Items.Add(new ComboBoxItem("タイル", View.Tile));

            var columnFileName = new ColumnHeader();
            columnFileName.Text = "名前";
            columnFileName.Width = 180;
            var columnFileSize = new ColumnHeader();
            columnFileSize.Text = "サイズ";
            columnFileSize.Width = 70;
            var columnLastWriteTime = new ColumnHeader();
            columnLastWriteTime.Text = "更新日時";
            columnLastWriteTime.Width = 120;
            ColumnHeader[] colHeaderRegValue = { columnFileName, columnFileSize, columnLastWriteTime };
            listViewFile.Columns.AddRange(colHeaderRegValue);

            if (Properties.Settings.Default.WindowSize.Width > 0 && Properties.Settings.Default.WindowSize.Height > 0)
            {
                Size = Properties.Settings.Default.WindowSize;
            }
            splitContainerMain.SplitterDistance = Properties.Settings.Default.SplitterDistance;
            toolStripComboBoxViewStyle.SelectedIndex = Properties.Settings.Default.ViewStyleIndex;

            ComboBoxItem item = (ComboBoxItem)toolStripComboBoxViewStyle.Items[toolStripComboBoxViewStyle.SelectedIndex];
            listViewFile.View = item.View;
        }

        private void treeViewFile_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = GetSelectedNode(((TreeView)sender).Nodes);

            if (node != null)
            {
                SetCurrentNode(node.Nodes);
            }
        }

        private void listViewFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var clickedItem = ((ListView)sender).HitTest(e.Location).Item;

            if (clickedItem != null)
            {
                var treeNode = (TreeNode)clickedItem.Tag;
                var fullPath = (string)treeNode.Tag;

                if (Directory.Exists(fullPath))
                {
                    SetCurrentNode(treeNode.Nodes);
                }
                else
                {
                    // 関連付けがない場合、例外が投げられる
                    // その場合は単に無視する
                    try { System.Diagnostics.Process.Start(fullPath); } catch {}
                }
            }
        }

        private void treeViewFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var clickedItem = ((TreeView)sender).HitTest(e.Location).Node;

            if (clickedItem != null)
            {
                var fullPath = (string)clickedItem.Tag;

                if (!Directory.Exists(fullPath))
                {
                    try { System.Diagnostics.Process.Start(fullPath); } catch {}
                }
            }
        }

        private void toolStripMenuItemFileOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Open(openFileDialog.FileName);
            }
        }

        private void toolStripMenuItemFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItemEditCopy_Click(object sender, EventArgs e)
        {
            var files = new System.Collections.Specialized.StringCollection();

            foreach (ListViewItem item in listViewFile.Items)
            {
                if (item.Selected)
                {
                    files.Add((string)item.Tag);
                }
            }

            if (files.Count > 0)
            {
                Clipboard.SetFileDropList(files);
            }
        }

        private void toolStripMenuItemEditSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewFile.Items)
            {
                item.Selected = true;
            }
        }

        private void toolStripMenuItemToolOption_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItemHelpVersionInfo_Click(object sender, EventArgs e)
        {

        }

        private void splitContainerMain_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Properties.Settings.Default.SplitterDistance = ((SplitContainer)sender).SplitterDistance;
        }

        private void toolStripComboBoxViewStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBoxViewStyle.Selected)
            {
                ComboBoxItem item = (ComboBoxItem)toolStripComboBoxViewStyle.Items[toolStripComboBoxViewStyle.SelectedIndex];
                listViewFile.View = item.View;
            }
        }
    }
}
