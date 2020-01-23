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

namespace FileViewer.Decompressor
{
    public partial class WorkerProgressForm : Form
    {
        public class WorkerParameter
        {
            public IDecompressor Decompressor { get; set; }

            public string InputFilePath { get; set; }
            public string OutputDirectoryPath { get; set; }
        }

        public class WorkerResult
        {
            public TreeNode RootNode { get; set; }
            public List<Image> LargeIconList { get; set; }
            public List<Image> SmallIconList { get; set; }
        }

        public WorkerResult Result { get; set; }

        public bool Start(WorkerParameter param)
        {
            try
            {
                backgroundWorker.RunWorkerAsync(param);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void CreateNodes(string path, List<Image> largeIconList, List<Image> smallIconList, TreeNodeCollection nodes)
        {
            var index = largeIconList.Count;
            FileManager.Info fileInfo = FileManager.GetFileInfo(path);

            var key = FileManager.ComputeIconHash(fileInfo.SmallIcon);

            if (IconCacheDictionary.ContainsKey(key))
                index = IconCacheDictionary[key];
            else
            {
                IconCacheDictionary.Add(key, index);
                largeIconList.Add(fileInfo.LargeIcon);
                smallIconList.Add(fileInfo.SmallIcon);
            }

            var node = new TreeNode(Path.GetFileName(path));
            node.ImageIndex = node.SelectedImageIndex = index;
            node.Tag = fileInfo;
            nodes.Add(node);

            if (Directory.Exists(path))
            {
                foreach (var entry in Directory.GetDirectories(path, "*"))
                {
                    CreateNodes(entry, largeIconList, smallIconList, node.Nodes);
                }

                foreach (var entry in Directory.GetFiles(path, "*"))
                {
                    CreateNodes(entry, largeIconList, smallIconList, node.Nodes);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private IDecompressor Decompressor { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WorkerProgressForm()
        {
            InitializeComponent();

            IconCacheDictionary = new Dictionary<string, int>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decompressor"></param>
        /// <param name="srcFilePath"></param>
        /// <param name="dstDirectoryPath"></param>
        /// <returns></returns>
        public bool Decompress(IDecompressor decompressor, string srcFilePath, string dstDirectoryPath)
        {
            Decompressor = decompressor;

            try
            {
                var args = new List<string>
                {
                    srcFilePath,
                    dstDirectoryPath
                };
                backgroundWorker.RunWorkerAsync(args);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var param = e.Argument as WorkerParameter;

            if (param.Decompressor.Decompress(param.InputFilePath, param.OutputDirectoryPath))
            {
                if (Directory.Exists(param.OutputDirectoryPath))
                {
                    var result = new WorkerResult();
                    result.LargeIconList = new List<Image>();
                    result.SmallIconList = new List<Image>();
                    result.RootNode = new TreeNode();

                    foreach (var entry in Directory.GetDirectories(param.OutputDirectoryPath, "*"))
                    {
                        CreateNodes(entry, result.LargeIconList, result.SmallIconList, result.RootNode.Nodes);
                    }
                    foreach (var entry in Directory.GetFiles(param.OutputDirectoryPath, "*"))
                    {
                        CreateNodes(entry, result.LargeIconList, result.SmallIconList, result.RootNode.Nodes);
                    }

                    e.Result = result;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                DialogResult = DialogResult.Cancel;
            }

            FormClosing -= WorkerProgressForm_FormClosing;

            if (e.Result == null)
            {
                DialogResult = DialogResult.No;
            }
            else
            {
                Result = e.Result as WorkerResult;
                DialogResult = DialogResult.OK;
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }
        }

        /// <summary>
        /// アイコンキャッシュのための辞書
        /// </summary>
        private Dictionary<string, int> IconCacheDictionary { get; set; }
    }
}
