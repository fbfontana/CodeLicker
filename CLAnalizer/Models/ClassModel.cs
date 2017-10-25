using CLAnalyzer.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAnalyzer.Models
{
    public class ClassModel
    {
        public List<string> ParentFiles { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public ValidatedInt MethodTotal { get; set; }
        public ObservableCollection<string> Methods { get; set; }

        public bool IsValid
        {
            get
            {
                return true;// MethodTotal.IsValid;
            }
            set
            {

            }
        }

        public ClassModel()
        {
            MethodTotal = new ValidatedInt(0, 16);
        }

        public ClassModel(List<string> filePaths, string name, string currentNamespace) : base()
        {
            ParentFiles = filePaths;
            Name = name;
            Namespace = currentNamespace;
            Methods = ClassAnalyzer.GetAllMethodNames(filePaths, name, currentNamespace);
            MethodTotal = new ValidatedInt();
            MethodTotal.Value = Methods.Count();
        }



}
}
