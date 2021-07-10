using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace PT_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];
            string sortType = args[1];
            string reverse = args[2];
            int level = 0;

            Sort(path, sortType, reverse, args, level);
        }
        static void Print(string __path, string __sorttype, string __reverse, string[] __args, int __level, Dictionary<Element, string> __sortedElementFilesPrint, Dictionary<Element, string> __sortedElementDirectoriesPrint)
        {
            string indentation = " ";
            for (int i = 0; i < __level; i++)
            {
                indentation  += '\t';
            }

            if (__sortedElementFilesPrint != null || __sortedElementDirectoriesPrint != null)
            {
                foreach (var e in __sortedElementFilesPrint)
                {
                    Console.WriteLine(indentation + e.Key.GetName() + " - " + e.Key.GetSize() + "kB - " + e.Key.GetAttributes() + " - " + e.Key.GetDateModification());
                }
                foreach (var d in __sortedElementDirectoriesPrint)
                {
                    Console.WriteLine(indentation + d.Key.GetName() + " - " + d.Key.GetSize() + "f - " + d.Key.GetAttributes() + " - " + d.Key.GetDateModification());
                    Sort(__path + '\\' + d.Key.GetName(), __sorttype, __reverse, __args, __level);
                }
            }
        }
        static void Sort(string __path, string __sorttype, string __reverse, string[] __args, int __level)
        {
            __level++;
            IEnumerable<string> unsortedElementFiles = Directory.EnumerateFiles(__path, "*");
            IEnumerable<string> unsortedElementDirectories = Directory.EnumerateDirectories(__path, "*");

            // SortedDictionary<TKey, TValue>
            // SortedDictionary<Element, string> sortedElementFiles = new SortedDictionary<Element, string>(new SortByName());
            SortedDictionary<Element, string> sortedElementFiles = new SortedDictionary<Element, string>(new SortByName());
            SortedDictionary<Element, string> sortedElementDirectories = new SortedDictionary<Element, string>(new SortByName());

            foreach (var f in unsortedElementFiles)
            {
                sortedElementFiles = SetFile(f, sortedElementFiles, __level);
            }

            foreach (var d in unsortedElementDirectories)
            {
                sortedElementDirectories = SetDirectory(d, sortedElementDirectories, __level);
            }

            var sortedElementFilesPrint = new Dictionary<Element, string>(sortedElementFiles);
            var sortedElementDirectoriesPrint = new Dictionary<Element, string>(sortedElementDirectories);

            switch (__sorttype)
            {
                case "byName": //byName 
                    {
                        // Console.WriteLine("Sorting by Name!");
                        if (__reverse == "Reverse")
                        {
                            sortedElementFilesPrint = SortReverse(sortedElementFilesPrint);
                            sortedElementDirectoriesPrint = SortReverse(sortedElementDirectoriesPrint);
                        }
                        break;
                    }

                case "bySize": //bySize
                    {
                        sortedElementFilesPrint = SortBySize(sortedElementFiles);
                        sortedElementDirectoriesPrint = SortBySize(sortedElementDirectories);
                        if (__reverse == "Reverse")
                        {
                            sortedElementFilesPrint = SortReverse(sortedElementFilesPrint);
                            sortedElementDirectoriesPrint = SortReverse(sortedElementDirectoriesPrint);
                        }
                        break;
                    }

                case "byDate": //byDate
                    {
                        sortedElementFilesPrint = SortByDate(sortedElementFiles);
                        sortedElementDirectoriesPrint = SortByDate(sortedElementDirectories);
                        if (__reverse == "Reverse")
                        {
                            sortedElementFilesPrint = SortReverse(sortedElementFilesPrint);
                            sortedElementDirectoriesPrint = SortReverse(sortedElementDirectoriesPrint);
                        }
                        break;
                    }

                default:
                    {
                        break;
                    }
            }

            Print(__path, __sorttype, __reverse, __args, __level, sortedElementFilesPrint, sortedElementDirectoriesPrint);
        }

        private static SortedDictionary<Element, string> SetFile(string __path, SortedDictionary<Element, string> __sortedElementFiles, int __level)
        {
            Element newFile = new Element(__path, ElementType.File, __level);
            if (!__sortedElementFiles.ContainsKey(newFile))
            {
                __sortedElementFiles.Add(newFile, newFile.GetName());
            }
            return __sortedElementFiles;
        }
        private static SortedDictionary<Element, string> SetDirectory(string __path, SortedDictionary<Element, string> __sortedElementDirectories, int __level)
        {
            Element newDirectory = new Element(__path, ElementType.Folder, __level);
            if (!__sortedElementDirectories.ContainsKey(newDirectory))
            {
                __sortedElementDirectories.Add(newDirectory, newDirectory.GetName());
            }
            return __sortedElementDirectories;
        }
        public class SortByName : IComparer<Element>
        {
            public int Compare(Element x, Element y)
            {
                return x.GetName().CompareTo(y.GetName());
            } 
        }
        static Dictionary <Element, string>  SortBySize(SortedDictionary<Element, string> __sortedElement)
        {
            return __sortedElement.OrderBy(x => x.Key.GetSize()).ToDictionary(x => x.Key, x => x.Value);
        }
        static Dictionary<Element, string> SortByDate(SortedDictionary<Element, string> __sortedElement)
        {
            return __sortedElement.OrderBy(x => x.Key.GetDateModification()).ToDictionary(x => x.Key, x => x.Value);
        }
        static Dictionary<Element, string> SortReverse(Dictionary<Element, string> __sortedElement)
        {
            // Console.WriteLine("Sorting with Reverse!");
            return __sortedElement.Reverse().ToDictionary(x => x.Key, x => x.Value);
        }
    }
}