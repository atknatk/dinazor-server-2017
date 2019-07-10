using System.Threading.Tasks;
using Dinazor.Core.Common.Model;

namespace Dinazor.Core.GenericOperation.Controller
{
    public interface IDeleteController
    {
        Task<DinazorResult> Delete(string token, long id);
    }
}
