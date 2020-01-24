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
        /// <summary>
        /// 解凍できるファイルの拡張子
        /// </summary>
        public string Extension => ".zip";

        /// <summary>
        /// 解凍できるファイル種類名
        /// </summary>
        public string Name => "ZIPファイル";

        /// <summary>
        /// 解凍
        /// </summary>
        /// <param name="srcFilePath"></param>
        /// <param name="dstDirectoryPath"></param>
        /// <returns></returns>
        public bool Decompress(string srcFilePath, string dstDirectoryPath)
        {
            return DecompressProcess.Start(
                @"..\..\Tool\TinyUnzipper.exe",
                new List<string> { srcFilePath, dstDirectoryPath }
            );
        }
    }
}
