using System;
using System.Collections.Generic;
using System.Reflection;
using Dinazor.Core.Common.Attributes;
using Dinazor.Core.Common.Helper;

namespace Dinazor.Core.IoC.Module
{
    public class DinazorModuleInfo
    {
        public Assembly Assembly { get; }
        public Type Type { get; }
        public DinazorModule Instance { get; }
        public List<DinazorModuleInfo> Dependencies { get; }
        public DinazorModuleInfo([Annotations.NotNullAttribute] Type type, [Annotations.NotNullAttribute] DinazorModule instance)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(instance, nameof(instance));

            Type = type;
            Instance = instance;
            Assembly = Type.Assembly;

            Dependencies = new List<DinazorModuleInfo>();
        }
        public override string ToString()
        {
            return Type.AssemblyQualifiedName ?? Type.FullName;
        }
    }
}