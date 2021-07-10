using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace System.IO
{
    public static class DirectoryInfoExtension
    {
        public static int GetSizeDirectory(this DirectoryInfo __folder)
        {
            return __folder.GetDirectories().Length + __folder.GetFiles().Length;
        }
    }

    public static class FileSystemInfoExtension
    {
        public static string GetAttribute(this FileSystemInfo __file)
        {
            string attributes = "";
            
            if (__file.Attributes.HasFlag(FileAttributes.System))
                attributes += "s";
            else
                attributes += "-";

            if (__file.Attributes.HasFlag(FileAttributes.ReadOnly))
                attributes += "r";
            else
                attributes += "-";

            if (__file.Attributes.HasFlag(FileAttributes.Hidden))
                attributes += "h";
            else
                attributes += "-";

            if (__file.Attributes.HasFlag(FileAttributes.Archive))
                attributes += "a";
            else
                attributes += "-";
            
            return attributes;
        }
    }

}
