using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeLicker.Properties;
using CodeLicker.ViewModels;
using CLAnalyzer.Models;
using CLAnalyzer;
using System.Windows.Input;
using CodeLicker.Helpers;

namespace CodeLicker.ViewModels
{
    public class ClassActivityVM : ActivityVM
    {
        #region Constants
        readonly string ActivityName = Resources.ClassName;
        #endregion
        #region Fields
        string _FilePath = string.Empty;
        string _Extension = string.Empty;
        #endregion
        #region Properties
        public ClassModel CurrentClass { get; set; }
        public ObservableCollection<string> Methods { get; private set; }
        public string IsValid { get; set; }
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public List<FileModel> ParentFiles { get; set; }
        public int MethodTotal { get; set; }
        #endregion
        public ClassActivityVM()
        {
            TabName = ActivityName;
        }

        public ClassActivityVM(ClassModel currentClass) : this()
        {
            TabName = Path.GetFileName(currentClass.Name);
            CurrentClass = currentClass;
            Namespace = currentClass.Namespace;
            ClassName = currentClass.Name;
            MethodTotal = currentClass.MethodTotal.Value;
            Methods = CurrentClass.Methods;
            IsValid = currentClass.IsValid ? Resources.Yes : Resources.No;
        }
    }
}
