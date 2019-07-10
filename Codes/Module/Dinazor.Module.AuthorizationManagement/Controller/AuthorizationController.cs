using System.Threading.Tasks;
using System.Web.Http;
using Dinazor.Core.Common.Model;
using Dinazor.Core.GenericOperation.Controller;
using Dinazor.Core.Interfaces.Token;
using Dinazor.Core.IoC;
using Dinazor.Core.Model;

namespace Dinazor.Module.AuthorizationManagement.Controller
{
    public class AuthorizationController : DinazorController
    {

        private readonly ITokenManager _tokenManager;

        public AuthorizationController()
        {
            _tokenManager = IocManager.Instance.Resolve<ITokenManager>();
        }

        [HttpPost]
        [ActionName("Default")]
        public async Task<DinazorResult> Login([FromBody] BaseTokenUser user)
        {
            return await _tokenManager.Login(user);
        }

        [HttpDelete] 
        public DinazorResult LogOut(string token)
        {
            return _tokenManager.LogOut(token);
        }
    }
}
