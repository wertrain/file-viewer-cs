using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileViewer.Decompressor
{
    /// <summary>
    /// 解凍機能インターフェース
    /// </summary>
    public interface IDecompressor
    {
        /// <summary>
        /// 解凍する
        /// </summary>
        /// <param name="srcFilePath"></param>
        /// <param name="dstDirectoryPath"></param>
        /// <returns></returns>
        bool Decompress(string srcFilePath, string dstDirectoryPath);

        /// <summary>
        /// 解凍できるファイルの拡張子
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// 解凍できるファイルのタイプ名
        /// </summary>
        string Name { get; }
    }
}
