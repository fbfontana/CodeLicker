using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        #region Fields
        string _FilePath = string.Empty;
        string _Extension = string.Empty;
        #endregion
        #region Properties
        public string FilePath
        {
            get
            {
                return _FilePath;
            }
            set
            {
                if (_FilePath == value)
                {
                    return;
                }
                _FilePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }
        public string Extension
        {
            get
            {
                return _Extension;
            }
            set
            {
                if (_Extension == value)
                {
                    return;
                }
                _Extension = value;
                OnPropertyChanged(nameof(Extension));
            }
        }
        #endregion
        public FileActivityVM()
        {
            TabName = ActivityName;
        }

        public FileActivityVM(string filename) : this()
        {
            TabName = Path.GetFileName(filename);
            FilePath = filename;
            Extension = Path.GetExtension(filename).ToUpper().Equals(".CS") ? Resources._CS : "";


        }
    }
}
