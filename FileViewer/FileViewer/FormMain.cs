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
        public FormMain()
        {
            InitializeComponent();

            Open(@"+");
        }

        private bool Open(string path)
        {
            listViewFile.View = View.LargeIcon;

            CreateNode(path, treeViewFile.Nodes);
            SetCurrentNode(treeViewFile.Nodes);

            return true;
        }

        private void SetCurrentNode(TreeNodeCollection nodes)
        {
            listViewFile.Items.Clear();

            foreach (TreeNode node in nodes)
            {
                var item = new ListViewItem(node.Text);
                item.ImageIndex = node.ImageIndex;
                item.Tag = node.Tag;
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
                foreach (var entry in Directory.GetFileSystemEntries(path, "*"))
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

        private void treeViewFile_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = GetSelectedNode(((TreeView)sender).Nodes);

            if (node != null && node.Nodes.Count > 0)
            {
                SetCurrentNode(node.Nodes);
            }
        }

        private void listViewFile_DoubleClick(object sender, EventArgs e)
        {

        }

        private void listViewFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var clickedItem = ((ListView)sender).HitTest(e.Location).Item;

            if (clickedItem != null)
            {
                System.Diagnostics.Process.Start((string)clickedItem.Tag);
            }
        }

        private void treeViewFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var clickedItem = ((TreeView)sender).HitTest(e.Location).Node;

            if (clickedItem != null)
            {
                System.Diagnostics.Process.Start((string)clickedItem.Tag);
            }
        }
    }
}
