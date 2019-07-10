using Dinazor.Core.Interfaces.Configuration;
using Dinazor.Core.IoC.Module;
using System.Web.Http.Controllers;
using Dinazor.Module.ConfigurationManagement.Manager;
using Dinazor.Module.ConfigurationManagement.BusinessLayer;
using Castle.MicroKernel.Registration;

namespace Dinazor.Module.ConfigurationManagement
{
    public class ConfigurationModule : DinazorModule
    {
        public override void PreInitialize()
        {
            IocManager.IocContainer.Register(
                Classes.FromThisAssembly().BasedOn<IHttpController>().LifestylePerWebRequest());
        }

        public override void Initialize()
        {
            IocManager.Register<IConfigurationOperation, ConfigurationOperation>();
            IocManager.Register<IConfigurationManager, ConfigurationManager>();
        }
    }
}
