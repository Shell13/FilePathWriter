using System.Collections.Generic;
using FilePathWriter.SearchOperation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilePathWriter.Tests.SearchOperationTest
{
    [TestClass()]
    public class Reversed2SearchOperationTests
    {
        [TestMethod()]
        public void ReversePathString_Test()
        {
            //arrange
            string path = "C:\\Users\\Some_User\\AppData\\Local\\Temp\\tmp1457.tmp";
            string expectedPath = "pmt.7541pmt\\pmeT\\lacoL\\ataDppA\\resU_emoS\\sresU\\:C";
            List<string> list = new List<string> { path };
            Reversed2SearchOperation reversed2 = new Reversed2SearchOperation();

            //act
            List<string> reversePath = reversed2.ReversePathString(list);

            //accert
            Assert.AreEqual(expectedPath, reversePath[0]);
        }
    }
}