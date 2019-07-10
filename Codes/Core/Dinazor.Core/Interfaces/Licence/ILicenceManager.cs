using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model; 
using Dinazor.Core.Dto.Crm;
using Dinazor.Core.GenericOperation.Interfaces;

namespace Dinazor.Core.Interfaces.Licence
{
    public interface ILicenceManager : IBaseDinazorManager
    {
        Task<DinazorResult<List<OrganizationLicenceDto>>> IsLicenceValid(ClientDto clientDto);
    }
}
