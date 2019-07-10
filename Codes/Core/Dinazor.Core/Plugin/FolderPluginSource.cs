
using System;
using System.Collections.Generic;
using System.IO;
using Dinazor.Core.Common.Extensions;
using Dinazor.Core.Common.Helper;
using Dinazor.Core.Exceptions;
using Dinazor.Core.Interfaces.Plugin;
using Dinazor.Core.IoC.Module;

namespace Dinazor.Core.Plugin
{
    public class FolderPluginSource : IPluginSource
    {
        public string Folder { get; }

        public SearchOption SearchOption { get; set; }

        public FolderPluginSource(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            Folder = folder;
            SearchOption = searchOption;
        }

        public List<Type> GetModules()
        {
            var modules = new List<Type>();

            var assemblies = AssemblyHelper.GetAllAssembliesInFolder(Folder, SearchOption);
            foreach (var assembly in assemblies)
            {
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (DinazorModule.IsDinazorModule(type))
                        {
                            modules.AddIfNotContains(type);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    throw new DinazorException("Could not get module types from assembly: " + assembly.FullName, ex);
                }
            }

            return modules;
        }
    }
}
