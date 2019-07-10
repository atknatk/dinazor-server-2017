
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;

namespace Dinazor.Core.GenericOperation.Interfaces
{
    public interface IDinazorDeleteManager : IBaseDinazorManager
    {
        Task<DinazorResult> Delete(long id);
    }
}
