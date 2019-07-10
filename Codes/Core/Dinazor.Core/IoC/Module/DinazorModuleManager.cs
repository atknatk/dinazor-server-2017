using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq; 
using Castle.Core.Logging;
using Dinazor.Core.Common.Extensions;
using Dinazor.Core.Interfaces.IoC;
using Dinazor.Core.Interfaces.Plugin;

namespace Dinazor.Core.IoC.Module
{
    public class DinazorModuleManager : IDinazorModuleManager
    {
        public DinazorModuleInfo StartupModule { get; private set; }

        public IReadOnlyList<DinazorModuleInfo> Modules => _modules.ToImmutableList();

        public ILogger Logger { get; set; }

        private DinazorModuleCollection _modules;

        private readonly IIocManager _iocManager;
        private readonly IDinazorPluginManager _dinazorPluginManager;

        public DinazorModuleManager(IIocManager iocManager, IDinazorPluginManager dinazorPluginManager
            )
        {
            _iocManager = iocManager;
            _dinazorPluginManager = dinazorPluginManager;
            Logger = NullLogger.Instance;
        }

        public virtual void Initialize(Type startupModule)
        {
            _modules = new DinazorModuleCollection(startupModule);
            LoadAllModules();
        }

        public virtual void StartModules()
        {
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.ForEach(module => module.Instance.PreInitialize());
            sortedModules.ForEach(module => module.Instance.Initialize());
            sortedModules.ForEach(module => module.Instance.PostInitialize());
        }

        public virtual void ShutdownModules()
        {
            Logger.Debug("Shutting down has been started");

            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.Reverse();
            sortedModules.ForEach(sm => sm.Instance.Shutdown());

            Logger.Debug("Shutting down completed.");
        }

        private void LoadAllModules()
        {
            Logger.Debug("Loading Dinazor modules...");

            var moduleTypes = FindAllModules().Distinct().ToList();

            Logger.Debug("Found " + moduleTypes.Count + " Dinazor modules in total.");

            RegisterModules(moduleTypes);
            CreateModules(moduleTypes);

            _modules.EnsureKernelModuleToBeFirst();
            _modules.EnsureStartupModuleToBeLast();

            SetDependencies();

            Logger.DebugFormat("{0} modules loaded.", _modules.Count);
        }

        private List<Type> FindAllModules()
        {
            var modules = DinazorModule.FindDependedModuleTypesRecursivelyIncludingGivenModule(_modules.StartupModuleType);

            _dinazorPluginManager
                .PluginSources
                .GetAllModules()
                .ForEach(m => modules.AddIfNotContains(m));

            return modules;
        }

        private void CreateModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                var moduleObject = _iocManager.Resolve(moduleType) as DinazorModule;
                if (moduleObject == null)
                {
                    throw  new Exception("create modules");
                   // throw new IsimInitializationException("This type is not an iSIM module: " + moduleType.AssemblyQualifiedName);
                }

                moduleObject.IocManager = _iocManager;
                var moduleInfo = new DinazorModuleInfo(moduleType, moduleObject);
                _modules.Add(moduleInfo);
                if (moduleType == _modules.StartupModuleType)
                {
                    StartupModule = moduleInfo;
                }
                Logger.DebugFormat("Loaded module: " + moduleType.AssemblyQualifiedName);
            }
        }

        private void RegisterModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                _iocManager.RegisterIfNot(moduleType);
            }
        }

        private void SetDependencies()
        {
            foreach (var moduleInfo in _modules)
            {
                moduleInfo.Dependencies.Clear();

                //Set dependencies for defined DependsOnAttribute attribute(s).
                foreach (var dependedModuleType in DinazorModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                    {
                        throw new Exception("set dependencies");
                        //throw new IsimInitializationException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                    }

                    if ((moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null))
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }
    }
}
