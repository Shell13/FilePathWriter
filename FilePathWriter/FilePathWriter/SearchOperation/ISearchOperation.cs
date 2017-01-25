using System.Collections.Generic;

namespace FilePathWriter.SearchOperation
{
    public interface ISearchOperation
    {
        List<string> GetPathList(string dirSrc);
    }
}
