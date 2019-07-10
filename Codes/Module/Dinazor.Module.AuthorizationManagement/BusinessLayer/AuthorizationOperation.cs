using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Database.Context;
using Dinazor.Core.Database.Entity;
using Dinazor.Core.Database.Entity.User;
using Dinazor.Core.Interfaces.Token;
using Dinazor.Core.Model;

namespace Dinazor.Module.AuthorizationManagement.BusinessLayer
{
    public class AuthorizationOperation : IAuthorizationOperation
    {
        public async Task<DinazorResult<TokenUser>> Login(BaseTokenUser user)
        {
            var result = new DinazorResult<TokenUser>();
            using (var ctx = new DinazorContext())
            {
                //TODO Hash(password)
                var exists = await
                    ctx.User.AnyAsync(l => !l.IsDeleted && l.Username == user.Username && l.Password == user.Password);
                if (exists)
                {
                    // get more detail later
                    var userEntity =
                        ctx.User.FirstOrDefault(
                            l => !l.IsDeleted && l.Username == user.Username && l.Password == user.Password);
                    result.Data = ToTokenUser(userEntity);
                    result.Success();
                }
                else
                {
                    //log
                    result.Status = ResultStatus.LoginFailed;
                    return result;

                }
                return result;
            }
        }

        private TokenUser ToTokenUser(User user)
        {
            var tokenUser = new TokenUser();
            tokenUser.Id = user.Id;
            tokenUser.Username = user.Username;

            return tokenUser;
        }
    }
}