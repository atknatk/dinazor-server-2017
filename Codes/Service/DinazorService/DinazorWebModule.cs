using System.Web.Http;
using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Dinazor.Core.IoC;
using Dinazor.Core.IoC.Module;
using Dinazor.Module.AuthorizationManagement;
using Dinazor.Module.ConfigurationManagement;
using Dinazor.Module.GisManagement;
using Dinazor.Module.UserManagement;


namespace DinazorService
{
    [DependsOn(typeof(UserModule),typeof(AuthorizationModule),typeof(GisModule), typeof(ConfigurationModule))]
    public class DinazorWebModule  : DinazorModule
    {
        public override void PreInitialize()
        {
            IocManager.IocContainer.Register(Classes.FromThisAssembly()
                .BasedOn<IHttpController>()
                .LifestylePerWebRequest());

        }
        public override void PostInitialize()
        {
            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(IocManager.IocContainer);
        }
    }

}