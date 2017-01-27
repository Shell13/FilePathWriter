using System.Collections.Generic;
using System.IO;
using FilePathWriter.SearchOperation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilePathWriter.Tests.SearchOperationTest
{
    [TestClass]
    public class CppSearchOperationTests
    {
        private string mainDir;
        private string expectedPath;

        [TestMethod]
        public void GetPathList_Test()
        {
            CppSearchOperation cppOperation = new CppSearchOperation();
            List<string> list = cppOperation.GetPathList(mainDir);

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], " /"+expectedPath);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var sourceDir = Path.GetTempPath();

            var randomFileName1 = Path.GetRandomFileName();
            mainDir = Path.Combine(sourceDir, randomFileName1);
            Directory.CreateDirectory(mainDir);
            var path = Path.Combine(mainDir, randomFileName1);
            File.Create(path).Close();

            var randomFileName2 = Path.GetRandomFileName();
            randomFileName2 = randomFileName2.Substring(0, randomFileName2.LastIndexOf('.')) + ".cpp"; //change extension 
            var innerDir = Path.Combine(mainDir, randomFileName2);
            Directory.CreateDirectory(innerDir);
            expectedPath = Path.Combine(innerDir, randomFileName2);
            File.Create(expectedPath).Close();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            Directory.Delete(mainDir, true);
        }
    }
}