using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dinazor.Core.Common.Extensions;
using Dinazor.Core.Interfaces.IoC;

namespace Dinazor.Core.IoC.Module
{
    public abstract class DinazorModule
    {
        protected internal IIocManager IocManager { get; internal set; }

        public virtual void PreInitialize()
        {

        }
        public virtual void Initialize()
        {

        }
        public virtual void PostInitialize()
        {

        }
        public virtual void Shutdown()
        {

        }
        public virtual Assembly[] GetAdditionalAssemblies()
        {
            return new Assembly[0];
        }
        public static bool IsDinazorModule(Type type)
        {
            return
                type.IsClass &&
                !type.IsAbstract &&
                !type.IsGenericType &&
                typeof(DinazorModule).IsAssignableFrom(type);
        }
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsDinazorModule(moduleType))
            {
                // throw new IsimInitializationException("This type is not an Isim module: " + moduleType.AssemblyQualifiedName);
            }
            var list = new List<Type>();
            if (!moduleType.IsDefined(typeof(DependsOnAttribute), true)) return list;
            var dependsOnAttributes = moduleType.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
            foreach (var dependsOnAttribute in dependsOnAttributes)
            {
                list.AddRange(dependsOnAttribute.DependedModuleTypes);
            }
            return list;
        }

        public static List<Type> FindDependedModuleTypesRecursivelyIncludingGivenModule(Type moduleType)
        {
            var list = new List<Type>();
            AddModuleAndDependenciesResursively(list, moduleType);
            list.AddIfNotContains(typeof(DinazorKernelModule));
            return list;
        }

        private static void AddModuleAndDependenciesResursively(List<Type> modules, Type module)
        {
            if (!IsDinazorModule(module))
            {
                //  throw new IsimInitializationException("This type is not an Isim module: " + module.AssemblyQualifiedName);
            }

            if (modules.Contains(module))
            {
                return;
            }

            modules.Add(module);

            var dependedModules = FindDependedModuleTypes(module);
            foreach (var dependedModule in dependedModules)
            {
                AddModuleAndDependenciesResursively(modules, dependedModule);
            }
        }
    }
}
