using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FileViewer
{
    public class FileManager
    {
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

        public class IconUtility
        {
            // Struct used by SHGetFileInfo function
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

            private const uint SHGFI_ICON = 0x100;
            private const uint SHGFI_LARGEICON = 0x0;
            private const uint SHGFI_SMALLICON = 0x000000001;

            public static Image GetIconImage(string path, bool large)
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                SHGetFileInfo(
                    path,
                    0, ref shinfo, (uint)Marshal.SizeOf(shinfo),
                    SHGFI_ICON | (large ? SHGFI_LARGEICON : SHGFI_SMALLICON));

                if (shinfo.hIcon == IntPtr.Zero)
                {
                    return new Bitmap(16, 16);
                }
                return IconToImage(Icon.FromHandle(shinfo.hIcon));
            }

            private static Image IconToImage(Icon icon)
            {
                Bitmap b = new Bitmap(icon.Width, icon.Height);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.DrawIcon(icon, 0, 0);
                }
                return b;
            }
        }
    }
}
