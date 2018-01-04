using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeLicker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLicker.Infrastructure.Tests
{
    [TestClass()]
    public class RegistryManagerTests
    {
        private const string TestPath = @"c:\test.test";

        [TestMethod()]
        public void SavePathTest()
        {
            SavePath_SavesOne_ThrowsNoException();
            SavePath_SavesMaxCountPlusOne_FindsMaxCount();
        }

        private static void SavePath_SavesOne_ThrowsNoException()
        {
            RegistryManager.SavePath(TestPath);
            Assert.IsTrue(true);
        }

        private static void SavePath_SavesMaxCountPlusOne_FindsMaxCount()
        {
            RegistryManager.ClearPaths();

            for (int i = 0; i < RegistryManager.MaxStoredRecentFiles + 1; i++)
            {
                RegistryManager.SavePath(TestPath + i.ToString());
            }

            var Result = RegistryManager.ReadPaths();
            Assert.AreEqual(Result.Count(), RegistryManager.MaxStoredRecentFiles);

            for (int i = 0; i < RegistryManager.MaxStoredRecentFiles; i++)
            {
                Assert.AreEqual(Result.ElementAt<string>(i), TestPath + (i+1).ToString());
            }
        }

        [TestMethod()]
        public void ReadPathsTest()
        {
            RegistryManager.ClearPaths();
            RegistryManager.SavePath(TestPath);

            var Result = RegistryManager.ReadPaths();

            Assert.AreEqual(Result.Count(), 1);
            Assert.AreEqual(Result.First(), TestPath);
        }

        [TestMethod()]
        public void GetRecentFilesCountTest()
        {
            RegistryManager.ClearPaths();
            RegistryManager.SavePath(TestPath);

            var Result = RegistryManager.GetRecentFilesCount();
            Assert.AreEqual(Result, 1);
        }

        [TestMethod()]
        public void ClearPathsTest()
        {
            RegistryManager.ClearPaths();
            var Result = RegistryManager.GetRecentFilesCount();
            Assert.AreEqual(Result, 0);
        }
    }
}