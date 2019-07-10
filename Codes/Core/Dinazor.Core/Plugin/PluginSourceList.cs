using System;
using System.Collections.Generic;
using System.Linq; 
using Dinazor.Core.Common.Extensions;
using Dinazor.Core.Interfaces.Plugin;

namespace Dinazor.Core.Plugin 
{
    public class PluginSourceList : List<IPluginSource>
    {
        public List<Type> GetAllModules()
        {
            return this
                .SelectMany(pluginSource => pluginSource.GetModulesWithAllDependencies())
                .Distinct()
                .ToList();
        }
    }
}
