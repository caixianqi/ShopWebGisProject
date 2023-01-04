/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMicroService.DynamicCodeGen

 *文件名：  DynamicCompiler

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/30 10:35:54

 *描述：编译器

/************************************************************************************/

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ShopWebGisMicroService.DynamicCodeGen
{
    public class DynamicCompiler
    {
        /// <summary>
        /// 编译
        /// </summary>
        /// <param name="text">字符串代码</param>
        /// <param name="referencedAssemblies">引用依赖项包</param>
        /// <returns></returns>
        public Assembly Compile(string text, params Assembly[] referencedAssemblies)
        {
            var references = referencedAssemblies.Select(it => MetadataReference.CreateFromFile(it.Location));
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var assemblyName = "_" + Guid.NewGuid().ToString("D");
            var syntaxTrees = new SyntaxTree[] { CSharpSyntaxTree.ParseText(text) };
            var compilation = CSharpCompilation.Create(assemblyName, syntaxTrees, references, options);
            using (var stream = new MemoryStream())
            {
                var compilationResult = compilation.Emit(stream);
                if (compilationResult.Success)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    return Assembly.Load(stream.ToArray());
                }
                else
                {
                    var strError = new StringBuilder();
                    IEnumerable<Diagnostic> failures = compilationResult.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        strError.Append(string.Format("{0}: {1},", diagnostic.Id, diagnostic.GetMessage()));
                    }
                    throw new InvalidOperationException(strError.ToString());
                }

            }
        }
    }
}
