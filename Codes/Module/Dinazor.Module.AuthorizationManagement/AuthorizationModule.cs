using Castle.MicroKernel.Registration;
using Dinazor.Core.Interfaces.Token;
using Dinazor.Core.IoC.Module;
using Dinazor.Module.AuthorizationManagement.BusinessLayer;
using Dinazor.Module.AuthorizationManagement.Manager;
using Dinazor.Module.AuthorizationManagement.TokenStore;
using System.Web.Http.Controllers;
using Dinazor.Core.Common.Enum;

namespace Dinazor.Module.AuthorizationManagement
{
    public class AuthorizationModule : DinazorModule
    {
        public override void PreInitialize()
        {
            IocManager.IocContainer.Register(
                Classes.FromThisAssembly().BasedOn<IHttpController>().LifestylePerWebRequest());
        }

        public override void Initialize()
        {
            IocManager.Register<ITokenManager, AuthorizationManager>();
            IocManager.Register<IAuthorizationOperation, AuthorizationOperation>();
            IocManager.Register<ITokenStore, MemoryTokenStorer>();
        }
    }
}
