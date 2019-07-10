using System.Threading.Tasks;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Model;

namespace Dinazor.Core.Interfaces.Token
{
    public interface ITokenManager
    {
        Task<DinazorResult<TokenUser>> Login(BaseTokenUser user);
        DinazorResult LogOut(string token);

        DinazorResult<TokenUser> GetUserByToken(string token);

        DinazorResult IsAuthorized(string token,long role);
    }
}
