using Dinazor.Core.Common.Model;
using Dinazor.Core.Model;

namespace Dinazor.Core.Interfaces.Token
{
    public interface ITokenStore
    {
        DinazorResult StoreTheToken(string token, TokenUser user);
        DinazorResult DeleteTheToken(string token);
        DinazorResult IsTokenExists(string token);
        DinazorResult<TokenUser> GetUserByToken(string token);
        DinazorResult<bool> IsUserAlreadyLoggedIn(string username,string password);
        DinazorResult<TokenUser> GetUserByUsername(string username);
    }
}
