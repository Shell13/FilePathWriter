using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilePathWriter.Tests
{
    [TestClass]
    public class FileScannerTest
    {
        private static string mainDir;
        private string expectedPath1;
        private string expectedPath2;

        [TestMethod]
        public void FindAllFilesAsync_Test1()
        {
            List<string> list = FileScanner.FindAllFilesAsync(mainDir).Result;

            Assert.AreEqual(list.Count, 2);
            Assert.AreEqual(list[0], expectedPath1);
            Assert.AreEqual(list[1], expectedPath2);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            Directory.Delete(mainDir, true);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var sourceDir = Path.GetTempPath();

            var randomFileName1 = Path.GetRandomFileName();
            mainDir = Path.Combine(sourceDir, randomFileName1);
            Directory.CreateDirectory(mainDir);
            expectedPath1 = Path.Combine(mainDir, randomFileName1);
            File.Create(expectedPath1).Close();

            var randomFileName2 = Path.GetRandomFileName();
            var innerDir = Path.Combine(mainDir, randomFileName2);
            Directory.CreateDirectory(innerDir);
            expectedPath2 = Path.Combine(innerDir, randomFileName2);
            File.Create(expectedPath2).Close();
        }
    }
}
