using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLicker.Infrastructure
{
    public static class RegistryManager
    {
        private const string RegistryPath = @"Software\CodeLicker\CodeLicker\MRU";
        public const int MaxStoredRecentFiles = 2;

        public static void SavePath(string currentPath)
        {
            RegistryKey CurrentKey = GetMRUKey();

            var CurrentNames = CurrentKey.GetValueNames().ToList();

            foreach (var Name in CurrentNames)
            {
                if (CurrentKey.GetValue(Name).Equals(currentPath))
                {
                    CurrentKey.DeleteValue(Name);
                    CurrentNames.Remove(Name);
                    break;
                }
            }
            
            if (CurrentNames.Count() == MaxStoredRecentFiles)
            {
                CurrentKey.DeleteValue(CurrentNames.First());
            }

            int Position = CurrentNames.Count() == 0? 0: int.Parse(CurrentNames.Last()) + 1;
            CurrentKey.SetValue((Position).ToString(), currentPath);
        }

        public static void ClearPaths()
        {
            RegistryKey CurrentKey = GetMRUKey();

            var CurrentNames = CurrentKey.GetValueNames();

            foreach (var Entry in CurrentNames)
            {
                CurrentKey.DeleteValue(Entry);
            }
        }

        private static RegistryKey GetMRUKey()
        {
            RegistryKey CurrentKey = Registry.CurrentUser;
            foreach (var Folder in RegistryPath.Split('\\'))
            {
                CurrentKey.CreateSubKey(Folder);
                CurrentKey = CurrentKey.OpenSubKey(Folder, true);
            }

            return CurrentKey;
        }

        public static int GetRecentFilesCount()
        {
            RegistryKey CurrentKey = GetMRUKey();

            var CurrentNames = CurrentKey.GetValueNames();
            return CurrentNames.Count();
        }

        public static List<string> ReadPaths()
        {
            var Result = new List<string>();
            RegistryKey CurrentKey = GetMRUKey();

            var MRUs = GetMRUKey().GetValueNames();
            foreach (var PathIndex in MRUs)
            {
                Result.Add(CurrentKey.GetValue(PathIndex).ToString());
            }

            return Result;
        }

    }
}
