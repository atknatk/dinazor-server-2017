
using System;
using System.Collections.Generic;
using System.Linq;
using Dinazor.Core.Interfaces.Plugin;

namespace Dinazor.Core.Plugin
{
    public class PluginTypeListSource : IPluginSource
    {
        private readonly Type[] _moduleTypes;

        public PluginTypeListSource(params Type[] moduleTypes)
        {
            _moduleTypes = moduleTypes;
        }

        public List<Type> GetModules()
        {
            return _moduleTypes.ToList();
        }
    }
}
