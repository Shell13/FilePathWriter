﻿using System.Collections.Generic;

namespace FilePathWriter.SearchOperation
{
    public class AllSearchOperation : ISearchOperation
    {
        public List<string> GetPathList(string dirSrc)
        {
            var files = FileScanner.FindAllFilesAsync(dirSrc).Result;
            return files;
        }
    }
}
