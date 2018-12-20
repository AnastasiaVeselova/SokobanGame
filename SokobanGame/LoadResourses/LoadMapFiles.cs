using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SokobanGame
{
    public static class LoadFiles
    {
        public static List<string> LoadFilesFromDirectory(string directoryPath = null, string searchPattern = "*.txt")
        {
            //исправить:
            //любыми адекватными способами дает bin/debug..., поэтому пока так
            //

            if (directoryPath == null)
            {
                var curDir = Directory.GetCurrentDirectory();
                var baseDir = "SokobanGame";
                directoryPath = Path.Combine(curDir.Substring(0, curDir.LastIndexOf(baseDir)+ baseDir.Length), "Maps");
            }
               

            return Directory.EnumerateFiles(directoryPath, searchPattern)
                      .Select(File.ReadAllText).ToList();
        }
    }
}
