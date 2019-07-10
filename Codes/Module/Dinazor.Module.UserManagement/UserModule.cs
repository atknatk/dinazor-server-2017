using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Dinazor.Core.Interfaces.User;
using Dinazor.Core.IoC.Module;
using Dinazor.Module.UserManagement.BusinessLayer;
using Dinazor.Module.UserManagement.Manager;

namespace Dinazor.Module.UserManagement
{
    public class UserModule : DinazorModule
    {
        public override void PreInitialize()
        {
            IocManager.IocContainer.Register(
                Classes.FromThisAssembly().BasedOn<IHttpController>().LifestylePerWebRequest());
        }

        public override void Initialize()
        {
            IocManager.Register<IUserManager, UserManager>();
            IocManager.Register<IUserOperation, UserOperation>();

            IocManager.Register<IUserGroupManager, UserGroupManager>();
            IocManager.Register<IUserGroupOperation, UserGroupOperation>();

            IocManager.Register<IRoleManager, RoleManager>();
            IocManager.Register<IRoleOperation, RoleOperation>();

            IocManager.Register<IRoleGroupManager, RoleGroupManager>();
            IocManager.Register<IRoleGroupOperation, RoleGroupOperation>();

            IocManager.Register<IRelationalManager, RelationalManager>();
            IocManager.Register<IRelationalOperation, RelationalOperation>();
        }
    }
}
