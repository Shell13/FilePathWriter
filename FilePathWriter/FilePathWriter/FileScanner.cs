using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilePathWriter
{
    public static class FileScanner
    {
        public static async Task<List<string>> FindAllFilesAsync(string dirSrc, string extension = "*")
        {
            DirectoryInfo dir = new DirectoryInfo(dirSrc);
            List<string> pathList = new List<string>();

            try
            {
                pathList = await Task.Run(() => GetFilePathes(dir, extension));
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception: " + e.Message);
            }
            
            return pathList;
        }

        private static List<string> GetFilePathes(DirectoryInfo info, string extension)
        {
            List<string> pathList = new List<string>();

            IEnumerable<FileInfo> fileInfos = new List<FileInfo>();
            try
            {
                fileInfos = info.GetFiles(extension);
            }
            catch (UnauthorizedAccessException e)
            {
//                Console.WriteLine("Caught exception: " + e.Message);
            }

            //limitation handling of the search pattern
            // https://msdn.microsoft.com/en-us/library/wz42302f(v=vs.110).aspx#Anchor_2
            if (fileInfos.Any() && extension != "*")
            {
                var ext = extension.Substring(extension.IndexOf(".", StringComparison.CurrentCulture));
                fileInfos = fileInfos.Where(file => file.Extension == ext).ToArray();
            }

            var names = fileInfos.Select(f => f.FullName);
            pathList.AddRange(names);
            IEnumerable<DirectoryInfo> directories = new List<DirectoryInfo>();

            try
            {
                directories = info.GetDirectories();
            }
            catch (UnauthorizedAccessException e)
            {
//                Console.WriteLine("Caught exception: " + e.Message);
            }

            foreach (DirectoryInfo dir in directories)
            {
                var filePathes = GetFilePathes(dir, extension);
                pathList.AddRange(filePathes);
            }

            return pathList;
        }
    }
}
