using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinazor.Core.Interfaces.Plugin
{
    public interface IPluginSource
    {
        List<Type> GetModules();
    }
}
