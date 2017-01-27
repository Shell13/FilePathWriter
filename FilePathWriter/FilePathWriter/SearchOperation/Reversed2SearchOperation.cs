using System.Collections.Generic;
using System.Linq;

namespace FilePathWriter.SearchOperation
{
    public class Reversed2SearchOperation : ISearchOperation
    {
        public List<string> GetPathList(string dirSrc)
        {
            var files = FileScanner.FindAllFilesAsync(dirSrc).Result;
            files = ReversePathString(files);
            return files;
        }

        public List<string> ReversePathString(List<string> pathList)
        {
            List<string> result = new List<string>();

            foreach (string path in pathList)
            {
                char[] arr = path.ToCharArray().Reverse().ToArray();
                string reversedPath = string.Join("", arr);
                result.Add(reversedPath);
            }

            return result;
        }
    }
}
