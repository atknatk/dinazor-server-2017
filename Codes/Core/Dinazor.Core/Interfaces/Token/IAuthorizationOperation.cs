using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Model;

namespace Dinazor.Core.Interfaces.Token
{
    public interface IAuthorizationOperation
    {
        Task<DinazorResult<TokenUser>> Login(BaseTokenUser user);
    }
}
