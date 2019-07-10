using System;
using Dinazor.Core.Identity;
using Dinazor.Core.Interfaces.Token;
using Dinazor.Core.IoC;

namespace Dinazor.Core.Interceptor.Attribute
{
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class AuthorizationAttribute : System.Attribute
    {

        private readonly long _role;

        public AuthorizationAttribute(long role)
        {
            _role = role;
        }

        public bool IsAuthorizated()
        {
            var token = IdentityHelper.Instance.GetToken();
            ITokenManager tokenManager = IocManager.Instance.Resolve<ITokenManager>();
            var result = tokenManager.IsAuthorized(token, _role);
            return result.IsSuccess;
        }
    }
}
