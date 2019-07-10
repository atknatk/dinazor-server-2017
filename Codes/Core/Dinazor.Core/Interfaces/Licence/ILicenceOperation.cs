using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.Dto.Crm;

namespace Dinazor.Core.Interfaces.Licence
{
    public interface ILicenceOperation
    {
        Task<DinazorResult<long>> GetClientByClientIdentifier(string clientIdentifier);
        Task<DinazorResult<List<OrganizationLicenceDto>>> GetActiveLicence(long clientId);
    }
}
