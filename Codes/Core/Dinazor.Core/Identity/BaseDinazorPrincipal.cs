
using System.Security.Principal;

namespace Dinazor.Core.Identity
{
    public class BaseDinazorPrincipal : IPrincipal
    {
        protected BaseDinazorPrincipal(IIdentity identity)
        {
            Identity = identity;
        }
        public virtual IIdentity Identity { get; }

        public virtual bool IsInRole(string role)
        {
            return false;
        }
    }
}
