using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Autofac;
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
        private static IContainer container;

        public static void Main(string[] args)
        {
            ConfigureDependencies();

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
                var operation = container.ResolveNamed<ISearchOperation>(option);
                var executor = new SearchExecutor(operation, sourcePath);
                Console.WriteLine("Start ...");
                var result = executor.Execute();
                bool saved = WriteResult(savePath, result);

                if (saved)
                {
                    Console.WriteLine("Completed");
                }
            }
        }

        private static void ConfigureDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<AllSearchOperation>().Named<ISearchOperation>(All);
            builder.RegisterType<CppSearchOperation>().Named<ISearchOperation>(Cpp);
            builder.RegisterType<Reversed1SearchOperation>().Named<ISearchOperation>(Reversed1);
            builder.RegisterType<Reversed2SearchOperation>().Named<ISearchOperation>(Reversed2);
            container = builder.Build();
        }
        
        private static bool WriteResult(string writePath, IEnumerable<string> pathList)
        {
            try
            {
                File.Create(writePath).Close();
                File.WriteAllLines(writePath, pathList, Encoding.UTF8);
                Console.WriteLine("Saved here: " + writePath);
                return true;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Failed to save results at the path: " + writePath);
                Console.WriteLine(e.Message);
            }
            return false;
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