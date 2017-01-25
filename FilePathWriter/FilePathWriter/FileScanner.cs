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
            FileInfo[] fileInfos = await Task.Run(() => dir.GetFiles(extension, SearchOption.AllDirectories));
            if (extension != "*")
            {
                extension = extension.Substring(extension.IndexOf(".", StringComparison.Ordinal));
                fileInfos = fileInfos.Where(file => file.Extension == extension).ToArray();
            }
            List<string> list = fileInfos.Select(info => info.FullName.Substring(dirSrc.Length+1)).ToList();
            return list;
        }
    }
}
