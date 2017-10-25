using Microsoft.VisualStudio.TestTools.UnitTesting;
using CLAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CLAnalyzer.Tests
{
    [TestClass()]
    public class ClassAnalyzerTests
    {
        public string FilePath { get; set; }

        [TestInitialize]
        public void Setup()
        {
            FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Props", "ClassAnalyzer", "FileAnalyzer.cs");
        }

        [TestMethod()]
        public void GetAllMethodNamesTest()
        {
            var CurrentClass = FileAnalyzer.GetAllClasses(FilePath).First();
            var CurrentMethods = ClassAnalyzer.GetAllMethodNames(CurrentClass);
            var Result = CurrentMethods.Count() == 3 &&
                CurrentMethods.Contains("GetAllClassNames") &&
                CurrentMethods.Contains("GetAllClasses") &&
                CurrentMethods.Contains("Analyze");
            Assert.IsTrue(Result);
        }

        [TestMethod()]
        public void GetMethodParameterCountsTest()
        {
            var CurrentClass = FileAnalyzer.GetAllClasses(FilePath).First();
            var CurrentMethods = ClassAnalyzer.GetMethodParameterCounts(CurrentClass);
            var Result = CurrentMethods.Count() == 3 &&
                CurrentMethods["GetAllClassNames"] == 1 &&
                CurrentMethods["GetAllClasses"] == 1 &&
                CurrentMethods["Analyze"] == 1;
            Assert.IsTrue(Result);
        }
    }
}