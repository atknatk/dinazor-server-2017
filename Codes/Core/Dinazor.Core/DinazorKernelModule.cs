using System.Reflection;
using Dinazor.Core.IoC;
using Dinazor.Core.IoC.Module;

namespace Dinazor.Core
{
    public class DinazorKernelModule : DinazorModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly(),
                new ConventionalRegistrationConfig
                {
                    InstallInstallers = false
                });
        }

        public override void PostInitialize()
        {
            RegisterMissingComponents();
        }
        private static void RegisterMissingComponents()
        {

        }
    }
}
