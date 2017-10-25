using CLAnalyzer.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAnalyzer.Models
{
    public class FileModel
    {
        public string Path { get; set; }
        public ValidatedInt ClassTotal { get; set; }
        public ObservableCollection<ClassModel> Classes { get; set; }

        public bool IsValid
        {
            get
            {
                return ClassTotal.IsValid;
            }
        }

        public FileModel()
        {
            ClassTotal = new ValidatedInt(1, 1);
        }

        public FileModel(string path) : base()
        {
            Path = path;
        }



    }
}
