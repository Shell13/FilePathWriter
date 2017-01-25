using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FilePathWriter.SearchOperation;

namespace FilePathWriter
{
    public class Program
    {
        private const string All = "all";
        private const string Cpp = "cpp";
        private const string Reversed1 = "reversed1";
        private const string Reversed2 = "reversed2";

        static void Main(string[] args)
        {
            string sourcePath = args[0];
            string option = args[1];
            string savePath;
            if (args.Length == 3)
            {
                savePath = args[2];
            }
            savePath = sourcePath + "\\results.txt"; //todo change!!!

            bool valid = ValidatePath(sourcePath);
            valid &= ValidateOption(option);
//            valid &= ValidatePath(savePath);

            if (valid)
            {
                var operation = GetSearchOperation(option);
                SearchExecutor executor = new SearchExecutor(operation, sourcePath);
                List<string> result = executor.Execute();
                WriteResult(result);
            }
        }

        private static ISearchOperation GetSearchOperation(string option)
        {
            ISearchOperation result = null;

            switch (option)
            {
                case All:
                    result = new AllSearchOperation();
                    break;
                case Cpp:
                    result = new CppSearchOperation();
                    break;
                case Reversed1:
                    result = new Reversed1SearchOperation();
                    break;
                case Reversed2:
                    result = new Reversed2SearchOperation();
                    break;
            }
            return result;
        }

        //todo implement
        private static bool WriteResult(IEnumerable<string> pathList)
        {

            return true;
        }

        private static bool ValidateOption(string option)
        {
            string[] operations = {All, Cpp, Reversed1, Reversed2};

            if (!operations.Contains(option))
            {
                throw new ArgumentException("Invalid parameter: " + option);
            }
            return true;
        }

        private static bool ValidatePath(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new ArgumentException("Invalid parameter: " + path);
            }

            return true;
        }
    }
}