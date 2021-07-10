using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace PT_Lab1
{
    public enum ElementType
    {
        File,
        Folder
    }
    class Element : IComparable<Element>
    {
        private string PathElement { get; set; }
        private string Name { get; set;  }
        private string Attribute { get; set; }
        private long Size { get; set; }
        private ElementType Type { get; set; }
        private DateTime DateModification { get; set; }
        private int Level { get; set; }

        public Element()
        {

        }

        public Element(string __path, ElementType __type, int __level)
        {
            SetPath(__path);
            SetType(__type);
            SetName();
            SetAttribute();
            SetSize();
            SetDateModification();
            SetLevel(__level);

        }

        void SetPath(string __path)
        {
            PathElement = __path;
        }

        void SetType(ElementType __type)
        {
            if (__type == ElementType.File)
            {
                Type = ElementType.File;
            }
            if (__type == ElementType.Folder)
            {
                Type = ElementType.Folder;
            }
        }

        void SetName()
        {
            //Name = Path.Remove(0, Path.LastIndexOf('\\') + 1);
            Name = Path.GetFileName(PathElement);
        }

        void SetAttribute()
        {
            FileInfo fileInfo = new FileInfo(PathElement);
            Attribute = fileInfo.GetAttribute();
        }

        void SetSize()
        {
            if (Type == ElementType.File)
            {
                FileInfo fileInfo = new FileInfo(PathElement);
                Size = fileInfo.Length / 1024;
            }
            if (Type == ElementType.Folder)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(PathElement);
                Size = directoryInfo.GetSizeDirectory();
            }
        }

        void SetDateModification()
        {
            DateModification = Directory.GetLastWriteTime(PathElement);
        }
        
        void SetLevel(int __level)
        {
            Level = __level;
        }

        public string GetName()
        {
            return Name;
        }

        public string GetAttributes()
        {
            return Attribute;
        }

        public long GetSize()
        {
            return Size;
        }

        public int GetLevel()
        {
            return Level;
        }

        public DateTime GetDateModification()
        {
            return DateModification;
        }

        public int CompareTo(Element __element)
        {
            return __element.GetName().CompareTo(Name);
        }
    }
}


