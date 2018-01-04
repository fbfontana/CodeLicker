using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CLAnalyzer;
using System.Linq;
using System.IO;

namespace CLAnalyzerTests
{
    [TestClass]
    public class FileAnalyzerTests
    {
        public string FilePath { get; set; }

        [TestInitialize]
        public void Setup()
        {
            FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Props", "FileAnalyzer", "MainWindow.xaml.cs");
        }

        [TestMethod()]
        public void AnalyzeTest()
        {
            var Result = FileAnalyzer.Analyze(FilePath);
            Assert.IsTrue(Result.IsValid);
        }

        [TestMethod()]
        public void GetAllClassesTest()
        {
            GetAllClassesInCsFile_OpensInvalidFile_ReturnsEmpty();
            GetAllClassesInCsFile_OpensFile_ReturnsOne();
        }

        private void GetAllClassesInCsFile_OpensFile_ReturnsOne()
        {
            var Result = FileAnalyzer.GetAllClassNames(FilePath);
            Assert.AreEqual(Result.First().Name, "MainWindow");
        }

        private void GetAllClassesInCsFile_OpensInvalidFile_ReturnsEmpty()
        {
            var InvalidFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Props", "FileAnalyzer", "Untitled.png");

            var Result = FileAnalyzer.GetAllClassNames(InvalidFilePath);
            Assert.AreEqual(Result.Count(), 0);
        }

        [TestMethod()]
        public void GetAllClassNamesTest()
        {
            Assert.Fail();
        }

    }
}
