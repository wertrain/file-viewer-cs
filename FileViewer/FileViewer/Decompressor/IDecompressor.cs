using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileViewer.Decompressor
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDecompressor
    {
        bool Decompress(string srcFilePath, string dstDirectoryPath);
    }
}
