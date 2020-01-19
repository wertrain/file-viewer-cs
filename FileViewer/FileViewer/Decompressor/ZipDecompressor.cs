using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileViewer.Decompressor
{
    public class ZipDecompressor : IDecompressor
    {
        private string DecompressorPath { get; set; }

        /// <summary>
        /// コンストラクタ（非公開）
        /// </summary>
        private ZipDecompressor()
        {

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="toolPath"></param>
        public ZipDecompressor(string toolPath)
        {
            DecompressorPath = toolPath;
        }

        /// <summary>
        /// 解凍
        /// </summary>
        /// <param name="srcFilePath"></param>
        /// <param name="dstDirectoryPath"></param>
        /// <returns></returns>
        public bool Decompress(string srcFilePath, string dstDirectoryPath)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = DecompressorPath,
                    Arguments = srcFilePath + " " + dstDirectoryPath,
                    UseShellExecute = false
                }
            };
            if (process.Start())
            {
                process.WaitForExit();
                return true;
            }
            return false;
        }
    }
}
