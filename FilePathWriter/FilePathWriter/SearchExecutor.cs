using System.Collections.Generic;
using FilePathWriter.SearchOperation;

namespace FilePathWriter
{
    public class SearchExecutor
    {
        private readonly ISearchOperation operation;
        private readonly string dirSrc;

        public SearchExecutor(ISearchOperation operation, string dirSrc)
        {
            this.operation = operation;
            this.dirSrc = dirSrc;
        }

        public List<string> Execute()
        {
            return operation.GetPathList(dirSrc);
        }
    }
}
