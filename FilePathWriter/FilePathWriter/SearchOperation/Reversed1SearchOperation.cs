using System.Collections.Generic;
using System.Linq;

namespace FilePathWriter.SearchOperation
{
    public class Reversed1SearchOperation : ISearchOperation
    {
        public List<string> GetPathList(string dirSrc)
        {
            var files = FileScanner.FindAllFilesAsync(dirSrc).Result;
            files = ReversePath(files);
            return files;
        }

        public List<string> ReversePath(List<string> pathList)
        {
            List<string> result = new List<string>();

            foreach (string path in pathList)
            {
                string[] arr = path.Split('\\').Reverse().ToArray();
                string reversedPath = string.Join("\\", arr);
                result.Add(reversedPath);
            }

            return result;
        }
    }
}
