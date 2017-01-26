using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FilePathWriter.SearchOperation;

namespace FilePathWriter
{
    public class Program
    {
        private const string All = "all";
        private const string Cpp = "cpp";
        private const string Reversed1 = "reversed1";
        private const string Reversed2 = "reversed2";
        private const string DefaultFileName = "results.txt";

        public static void Main(string[] args)
        {
            var sourcePath = args[0];
            var option = args[1];
            var savePath = Path.Combine(sourcePath, DefaultFileName);
            if (args.Length == 3)
                savePath = args[2];

            var valid = ValidateSourcePath(sourcePath);
            valid &= ValidateOption(option);
            valid &= ValidateSavePath(savePath);

            if (valid)
            {
                var operation = GetSearchOperation(option);
                var executor = new SearchExecutor(operation, sourcePath);
                var result = executor.Execute();
                WriteResult(savePath, result);

                Console.WriteLine("Completed");
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

        private static void WriteResult(string writePath, IEnumerable<string> pathList)
        {
            File.Create(writePath).Close();
            File.WriteAllLines(writePath, pathList, Encoding.UTF8);
        }

        private static bool ValidateOption(string option)
        {
            string[] operations = {All, Cpp, Reversed1, Reversed2};

            if (!operations.Contains(option))
            {
                Console.WriteLine("Invalid option: " + option);
                return false;
            }
            return true;
        }

        private static bool ValidateSourcePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Invalid source path: " + path);
                return false;
            }
            return true;
        }

        private static bool ValidateSavePath(string path)
        {
            if (path != null)
            {
                var directoryName = Path.GetDirectoryName(path);
                var fileName = Path.GetFileName(path);

                if (!Directory.Exists(directoryName) || string.IsNullOrEmpty(fileName))
                {
                    Console.WriteLine("Invalid save path: " + path);
                    return false;
                }
            }
            return true;
        }
    }
}