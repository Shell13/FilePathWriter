using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FilePathWriter
{
    public static class FileScanner
    {
        private static readonly List<string> PathesList = new List<string>();

        public static async Task<List<string>> FindAllFilesAsync(string dirSrc, string extension = "*")
        {
            try
            {
                await Task.Run(() => GetFilePathes(new DirectoryInfo(dirSrc), extension));
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception: " + e.Message);
            }

            return PathesList;
        }

        private static async Task GetFilePathes(DirectoryInfo info, string extension)
        {
            try
            {
                IEnumerable<FileInfo> fileInfos = info.GetFiles(extension);

                //filter by extension
                //limitation handling of the search pattern
                // https://msdn.microsoft.com/en-us/library/wz42302f(v=vs.110).aspx#Anchor_2
                if (fileInfos.Any() && extension != "*")
                {
                    var ext = extension.Substring(extension.IndexOf(".", StringComparison.CurrentCulture));
                    fileInfos = fileInfos.Where(file => file.Extension == ext).ToArray();
                }

                var names = fileInfos.Select(f => f.FullName);
                PathesList.AddRange(names);
            }
            catch (UnauthorizedAccessException) { }

            try
            {
                foreach (DirectoryInfo dir in info.EnumerateDirectories())
                {
                    await Task.Run(() => GetFilePathes(dir, extension));
                }
            }
            catch (UnauthorizedAccessException) { }
        }
    }
}
