using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dinazor.Core.IoC.Module;

namespace Dinazor.Core.Interfaces.IoC
{
    public interface IDinazorModuleManager
    {
        DinazorModuleInfo StartupModule { get; }

        IReadOnlyList<DinazorModuleInfo> Modules { get; }

        void Initialize(Type startupModule);

        void StartModules();

        void ShutdownModules();
    }
}
