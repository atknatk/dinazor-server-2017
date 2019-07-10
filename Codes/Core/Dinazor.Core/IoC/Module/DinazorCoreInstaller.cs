using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Dinazor.Core.Interfaces.IoC;
using Dinazor.Core.Interfaces.Plugin;
using Dinazor.Core.Plugin;

namespace Dinazor.Core.IoC.Module
{
    public class DinazorCoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDinazorPluginManager, DinazorPluginManager>().ImplementedBy<DinazorPluginManager>().LifestyleSingleton(),
                Component.For<IDinazorModuleManager, DinazorModuleManager>().ImplementedBy<DinazorModuleManager>().LifestyleSingleton()
            );
        }
    }
}
