using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Dinazor.Core.Interfaces.Gis;
using Dinazor.Core.IoC.Module;
using Dinazor.Module.GisManagement.BusinessLayer;
using Dinazor.Module.GisManagement.Manager;

namespace Dinazor.Module.GisManagement
{
    public class GisModule : DinazorModule
    {
        public override void PreInitialize()
        {
            IocManager.IocContainer.Register(
                Classes.FromThisAssembly().BasedOn<IHttpController>().LifestylePerWebRequest());
        }

        public override void Initialize()
        {
            IocManager.Register<ICityManager, CityManager>();
            IocManager.Register<ICityOperation, CityOperation>();

            IocManager.Register<ITownManager, TownManager>();
            IocManager.Register<ITownOperation, TownOperation>();
        }
    }
}
