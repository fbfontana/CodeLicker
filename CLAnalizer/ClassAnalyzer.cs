using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using System.Collections.ObjectModel;

namespace CLAnalyzer
{
    public static class ClassAnalyzer
    {
        public static List<string> GetAllMethodNames(ClassDeclarationSyntax currentClass)
        {
            return GetAllMethods(currentClass).Select(e => e.Identifier.ToFullString().Trim()).ToList();
        }

        internal static List<MethodDeclarationSyntax> GetAllMethods(ClassDeclarationSyntax currentClass)
        {
            return currentClass.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();
        }

        public static Dictionary<string, int> GetMethodParameterCounts(ClassDeclarationSyntax currentClass)
        {
            var Result = new Dictionary<string, int>();
            GetAllMethods(currentClass).ForEach(e =>
                    Result.Add(e.Identifier.ToFullString(), e.ParameterList.Parameters.Count));
            return Result;
        }

        internal static ObservableCollection<MethodDeclarationSyntax> GetAllMethods(List<string> filePaths, string name, string currentNamespace)
        {
            var Methods = new List<MethodDeclarationSyntax>();
            foreach (var filePath in filePaths)
            {
                var ClassDeclarations = FileAnalyzer.GetAllClasses(filePath).Where(e =>
                e.Identifier.ToFullString().Trim().Replace(Environment.NewLine, "") == name &&
                e.FirstAncestorOrSelf<NamespaceDeclarationSyntax>().Name.ToFullString().Trim().Replace(Environment.NewLine, "") == currentNamespace
                );

                if (ClassDeclarations.Count() == 1)
                {
                    Methods.AddRange(GetAllMethods(ClassDeclarations.Single()));
                }
            }

            return new ObservableCollection<MethodDeclarationSyntax>(Methods);
        }

        public static ObservableCollection<string> GetAllMethodNames(List<string> filePaths, string name, string currentNamespace)
        {
            var Methods = GetAllMethods(filePaths, name, currentNamespace);

            return new ObservableCollection<string>(Methods.Select(e => e.Identifier.ToFullString().Trim().Replace(Environment.NewLine, "")).ToList());
        }
    }
}
