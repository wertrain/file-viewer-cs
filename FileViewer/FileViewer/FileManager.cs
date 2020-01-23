using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FileViewer
{
    public class FileManager
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool DestroyIcon(IntPtr handle);

        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_LARGEICON = 0x0;
        private const uint SHGFI_SMALLICON = 0x000000001;
        private const uint SHGFI_USEFILEATTRIBUTES = 0x10;
        private const uint SHGFI_TYPENAME = 0x400;
        private const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;

        /// <summary>
        /// ファイル情報
        /// </summary>
        public class Info
        {
            public Image LargeIcon { get; set; }
            public Image SmallIcon { get; set; }
            public string FileType { get; set; }
            public string FilePath { get; set; }
            public FileInfo FileInfo { get; set; }
        }

        /// <summary>
        /// 一時ディレクトリを作成して取得する
        /// </summary>
        /// <returns></returns>
        public static string GetTempDirectory()
        {
#if DEBUG
            return string.Empty;
#else
            var name = Path.GetTempPath() + Guid.NewGuid().ToString() + Path.DirectorySeparatorChar;
            try
            {
                Directory.CreateDirectory(name);
            }
            catch
            {
                return string.Empty;
            }
            return name;
#endif
        }

        /// <summary>
        /// ファイル・ディレクトリを削除
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Delete(string path)
        {
#if DEBUG
            return false;
#else
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                else if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch
            {
                return false;
            }

            return true;
#endif
        }

        /// <summary>
        /// ファイル情報を取得
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Info GetFileInfo(string filePath)
        {
            Info fileInfo = new Info();
            fileInfo.FilePath = filePath;

            {
                SHFILEINFO shinfo = new SHFILEINFO();
                SHGetFileInfo(
                    filePath,
                    0, ref shinfo, (uint)Marshal.SizeOf(shinfo),
                    SHGFI_TYPENAME |
                    SHGFI_ICON | SHGFI_LARGEICON);

                if (shinfo.hIcon == IntPtr.Zero)
                {
                    fileInfo.LargeIcon = new Bitmap(32, 32);
                }
                else
                {
                    fileInfo.LargeIcon = (Image)IconToImage(Icon.FromHandle(shinfo.hIcon));
                    DestroyIcon(shinfo.hIcon);
                }
                fileInfo.FileType = shinfo.szTypeName;
            }

            {
                SHFILEINFO shinfo = new SHFILEINFO();
                SHGetFileInfo(
                    filePath,
                    0, ref shinfo, (uint)Marshal.SizeOf(shinfo),
                    SHGFI_ICON | SHGFI_SMALLICON);

                if (shinfo.hIcon == IntPtr.Zero)
                {
                    fileInfo.SmallIcon = new Bitmap(16, 16);
                }
                else
                {
                    fileInfo.SmallIcon = IconToImage(Icon.FromHandle(shinfo.hIcon));
                    DestroyIcon(shinfo.hIcon);
                }
            }

            fileInfo.FileInfo = new FileInfo(filePath);
            
            return fileInfo;
        }

        /// <summary>
        /// アイコンをイメージに変換
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        private static Image IconToImage(Icon icon)
        {
            Bitmap b = new Bitmap(icon.Width, icon.Height);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawIcon(icon, 0, 0);
            }
            return b;
        }

        /// <summary>
        /// バイト数をバイト表記に変換
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        public static string BytesToString(long byteCount)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = byteCount;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return string.Format("{0:0.##} {1}", len, sizes[order]);
        }

        /// <summary>
        /// アイコンからハッシュを計算する
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static string ComputeIconHash(Image image)
        {
            ImageConverter converter = new ImageConverter();
            byte[] rawIcon = converter.ConvertTo(image, typeof(byte[])) as byte[];

            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(rawIcon);

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var h in hash)
            {
                stringBuilder.Append(h.ToString("X2"));
            }
            return stringBuilder.ToString();
        }
    }
}
