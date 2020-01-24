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

    /// <summary>
    /// プロセス実行ユーティリティ
    /// </summary>
    public class DecompressProcess
    {
        /// <summary>
        /// プロセスを実行
        /// </summary>
        /// <param name="fileName">実行するファイル名</param>
        /// <param name="args">引数のリスト（半角スペースで連結）</param>
        /// <returns></returns>
        public static bool Start(string fileName, List<string> args)
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = string.Join(" ", args),
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
