using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeLicker.Properties;
using CodeLicker.ViewModels;

namespace CodeLicker.ViewModels
{
    public class FileActivityVM : ActivityVM
    {
        #region Constants
        readonly string ActivityName = Resources.File;
        #endregion
        public FileActivityVM()
        {
            TabName = ActivityName;
        }

        public FileActivityVM(string filename) : this()
        {
            TabName = Path.GetFileName(filename);
        }
    }
}
