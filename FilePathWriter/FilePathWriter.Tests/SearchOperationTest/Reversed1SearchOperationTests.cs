using System.Collections.Generic;
using FilePathWriter.SearchOperation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilePathWriter.Tests.SearchOperationTest
{
    [TestClass]
    public class Reversed1SearchOperationTests
    {
        [TestMethod]
        public void ReversePath_Test()
        {
            //arrange
            string path = "C:\\Users\\Some_User\\AppData\\Local\\Temp\\tmp1457.tmp";
            string expectedPath = "tmp1457.tmp\\Temp\\Local\\AppData\\Some_User\\Users\\C:";
            List<string> list = new List<string> {path};
            Reversed1SearchOperation reversed1 = new Reversed1SearchOperation();

            //act
            List<string> reversePath = reversed1.ReversePath(list);

            //accert
            Assert.AreEqual(expectedPath, reversePath[0]);
        }
    }
}