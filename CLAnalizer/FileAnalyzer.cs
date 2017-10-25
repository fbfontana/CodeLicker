using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CLAnalyzer.Models;
using System.Collections.ObjectModel;

namespace CLAnalyzer
{
    public class FileAnalyzer
    {
        public static List<ClassModel> GetAllClassNames(string filePath)
        {
            return GetAllClasses(filePath).Select(e =>
                    new ClassModel(
                        new List<string>() { filePath },
                        e.Identifier.ToFullString().Trim().Replace(Environment.NewLine, ""),
                        e.FirstAncestorOrSelf<NamespaceDeclarationSyntax>().Name.ToFullString().Trim().Replace(Environment.NewLine, ""))
                ).ToList();
        }

        public static List<ClassDeclarationSyntax> GetAllClasses(string filePath)
        {
            var Code = File.ReadAllText(filePath);
            var Root = CSharpSyntaxTree.ParseText(Code).GetRoot();
            return Root.DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();
        }

        public static FileModel Analyze(string filePath)
        {
            var CurrentFile = new FileModel()
            {
                Path = filePath
            };
            CurrentFile.Classes = new ObservableCollection<ClassModel>(GetAllClassNames(filePath));
            CurrentFile.ClassTotal.Value = CurrentFile.Classes.Count();

            return CurrentFile;
        }


    }
}
