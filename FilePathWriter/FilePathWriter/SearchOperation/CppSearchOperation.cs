using System.Collections.Generic;
using System.Linq;

namespace FilePathWriter.SearchOperation
{
    public class CppSearchOperation : ISearchOperation
    {
        public List<string> GetPathList(string dirSrc)
        {
            var files = FileScanner.FindAllFilesAsync(dirSrc, "*.cpp").Result;
            files = files.Select(path => " /" + path).ToList();
            return files;
        }
    }
}
