using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Dinazor.Core.Interfaces.Licence; 
using Dinazor.Core.IoC.Module;
using Dinazor.Module.LicenceManagement.BusinessLayer;
using Dinazor.Module.LicenceManagement.Manager;

namespace Dinazor.Module.LicenceManagement
{
    public class LicenceModule : DinazorModule
    {
        public override void PreInitialize()
        {
            IocManager.IocContainer.Register(
                Classes.FromThisAssembly().BasedOn<IHttpController>().LifestylePerWebRequest());
        }

        public override void Initialize()
        {
            IocManager.Register<ILicenceOperation, LicenceOperation>();
            IocManager.Register<ILicenceManager,LicenceManager>();
        }
    }
}
