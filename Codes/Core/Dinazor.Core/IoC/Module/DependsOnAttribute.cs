using System; 

namespace Dinazor.Core.IoC.Module
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : System.Attribute
    {
        public Type[] DependedModuleTypes { get; private set; }
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}
