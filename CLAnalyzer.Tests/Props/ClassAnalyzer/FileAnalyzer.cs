using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CLAnalyzer.Models;

namespace CLAnalyzer
{
    public class FileAnalyzer
    {
        public static List<string> GetAllClassNames(string filePath)
        {
            return GetAllClasses(filePath).Select(e => e.Identifier.ToFullString().Trim()).ToList();
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
            CurrentFile.ClassTotal.Value = GetAllClassNames(filePath).Count();

            return CurrentFile;
        }


    }
}
