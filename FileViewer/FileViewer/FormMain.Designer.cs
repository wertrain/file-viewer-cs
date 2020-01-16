namespace FileViewer
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemFileOverwrite = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTool = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemToolOption = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelpVersionInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.treeViewFile = new System.Windows.Forms.TreeView();
            this.imageListSmallIcon = new System.Windows.Forms.ImageList(this.components);
            this.listViewFile = new System.Windows.Forms.ListView();
            this.imageListLargeIcon = new System.Windows.Forms.ImageList(this.components);
            this.menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemEdit,
            this.toolStripMenuItemTool,
            this.toolStripMenuItemHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(800, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStripMain";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFileOpen,
            this.toolStripSeparator,
            this.toolStripMenuItemFileOverwrite,
            this.toolStripMenuItemFileSaveAs,
            this.toolStripSeparator1,
            this.toolStripMenuItemFileExit});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(67, 20);
            this.toolStripMenuItemFile.Text = "ファイル(&F)";
            // 
            // toolStripMenuItemFileOpen
            // 
            this.toolStripMenuItemFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemFileOpen.Image")));
            this.toolStripMenuItemFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItemFileOpen.Name = "toolStripMenuItemFileOpen";
            this.toolStripMenuItemFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripMenuItemFileOpen.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItemFileOpen.Text = "開く(&O)";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(181, 6);
            // 
            // toolStripMenuItemFileOverwrite
            // 
            this.toolStripMenuItemFileOverwrite.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemFileOverwrite.Image")));
            this.toolStripMenuItemFileOverwrite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItemFileOverwrite.Name = "toolStripMenuItemFileOverwrite";
            this.toolStripMenuItemFileOverwrite.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItemFileOverwrite.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItemFileOverwrite.Text = "上書き保存(&S)";
            // 
            // toolStripMenuItemFileSaveAs
            // 
            this.toolStripMenuItemFileSaveAs.Name = "toolStripMenuItemFileSaveAs";
            this.toolStripMenuItemFileSaveAs.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItemFileSaveAs.Text = "名前を付けて保存(&A)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // toolStripMenuItemFileExit
            // 
            this.toolStripMenuItemFileExit.Name = "toolStripMenuItemFileExit";
            this.toolStripMenuItemFileExit.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItemFileExit.Text = "終了(&X)";
            // 
            // toolStripMenuItemEdit
            // 
            this.toolStripMenuItemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemEditCut,
            this.toolStripMenuItemEditCopy,
            this.toolStripMenuItemEditPaste,
            this.toolStripSeparator4,
            this.toolStripMenuItemEditSelectAll});
            this.toolStripMenuItemEdit.Name = "toolStripMenuItemEdit";
            this.toolStripMenuItemEdit.Size = new System.Drawing.Size(57, 20);
            this.toolStripMenuItemEdit.Text = "編集(&E)";
            // 
            // toolStripMenuItemEditCut
            // 
            this.toolStripMenuItemEditCut.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemEditCut.Image")));
            this.toolStripMenuItemEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItemEditCut.Name = "toolStripMenuItemEditCut";
            this.toolStripMenuItemEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.toolStripMenuItemEditCut.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItemEditCut.Text = "切り取り(&T)";
            // 
            // toolStripMenuItemEditCopy
            // 
            this.toolStripMenuItemEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemEditCopy.Image")));
            this.toolStripMenuItemEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItemEditCopy.Name = "toolStripMenuItemEditCopy";
            this.toolStripMenuItemEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItemEditCopy.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItemEditCopy.Text = "コピー(&C)";
            // 
            // toolStripMenuItemEditPaste
            // 
            this.toolStripMenuItemEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemEditPaste.Image")));
            this.toolStripMenuItemEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItemEditPaste.Name = "toolStripMenuItemEditPaste";
            this.toolStripMenuItemEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.toolStripMenuItemEditPaste.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItemEditPaste.Text = "貼り付け(&P)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(167, 6);
            // 
            // toolStripMenuItemEditSelectAll
            // 
            this.toolStripMenuItemEditSelectAll.Name = "toolStripMenuItemEditSelectAll";
            this.toolStripMenuItemEditSelectAll.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItemEditSelectAll.Text = "すべて選択(&A)";
            // 
            // toolStripMenuItemTool
            // 
            this.toolStripMenuItemTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemToolOption});
            this.toolStripMenuItemTool.Name = "toolStripMenuItemTool";
            this.toolStripMenuItemTool.Size = new System.Drawing.Size(60, 20);
            this.toolStripMenuItemTool.Text = "ツール(&T)";
            // 
            // toolStripMenuItemToolOption
            // 
            this.toolStripMenuItemToolOption.Name = "toolStripMenuItemToolOption";
            this.toolStripMenuItemToolOption.Size = new System.Drawing.Size(135, 22);
            this.toolStripMenuItemToolOption.Text = "オプション(&O)";
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemHelpVersionInfo});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(65, 20);
            this.toolStripMenuItemHelp.Text = "ヘルプ(&H)";
            // 
            // toolStripMenuItemHelpVersionInfo
            // 
            this.toolStripMenuItemHelpVersionInfo.Name = "toolStripMenuItemHelpVersionInfo";
            this.toolStripMenuItemHelpVersionInfo.Size = new System.Drawing.Size(167, 22);
            this.toolStripMenuItemHelpVersionInfo.Text = "バージョン情報(&A)...";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 24);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.treeViewFile);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.listViewFile);
            this.splitContainerMain.Size = new System.Drawing.Size(800, 426);
            this.splitContainerMain.SplitterDistance = 266;
            this.splitContainerMain.TabIndex = 1;
            // 
            // treeViewFile
            // 
            this.treeViewFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFile.ImageIndex = 0;
            this.treeViewFile.ImageList = this.imageListSmallIcon;
            this.treeViewFile.Location = new System.Drawing.Point(0, 0);
            this.treeViewFile.Name = "treeViewFile";
            this.treeViewFile.SelectedImageIndex = 0;
            this.treeViewFile.Size = new System.Drawing.Size(266, 426);
            this.treeViewFile.TabIndex = 0;
            this.treeViewFile.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFile_AfterSelect);
            this.treeViewFile.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeViewFile_MouseDoubleClick);
            // 
            // imageListSmallIcon
            // 
            this.imageListSmallIcon.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListSmallIcon.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListSmallIcon.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listViewFile
            // 
            this.listViewFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFile.HideSelection = false;
            this.listViewFile.LargeImageList = this.imageListLargeIcon;
            this.listViewFile.Location = new System.Drawing.Point(0, 0);
            this.listViewFile.Name = "listViewFile";
            this.listViewFile.Size = new System.Drawing.Size(530, 426);
            this.listViewFile.SmallImageList = this.imageListSmallIcon;
            this.listViewFile.TabIndex = 0;
            this.listViewFile.UseCompatibleStateImageBehavior = false;
            this.listViewFile.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewFile_MouseDoubleClick);
            // 
            // imageListLargeIcon
            // 
            this.imageListLargeIcon.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListLargeIcon.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListLargeIcon.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormMain";
            this.Text = "ファイルビューア";
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileOverwrite;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEditCut;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEditCopy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEditPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEditSelectAll;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTool;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemToolOption;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelpVersionInfo;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TreeView treeViewFile;
        private System.Windows.Forms.ListView listViewFile;
        private System.Windows.Forms.ImageList imageListSmallIcon;
        private System.Windows.Forms.ImageList imageListLargeIcon;
    }
}

