using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Crm;
using Dinazor.Core.Interfaces.Licence;

namespace Dinazor.Module.LicenceManagement.Manager
{
    public class LicenceManager : ILicenceManager
    {
        private readonly ILicenceOperation _licenceOperation;

        public LicenceManager(ILicenceOperation licenceOperation)
        {
            _licenceOperation = licenceOperation;
        }


        public async Task<DinazorResult<List<OrganizationLicenceDto>>> IsLicenceValid(ClientDto clientDto)
        {
            var result = new DinazorResult<List<OrganizationLicenceDto>>();
            // try to the generate the client identifier according to the given information
            var clientIdentifier = clientDto.BiosVersion + clientDto.HddSerialNo + clientDto.Username + clientDto.Password;
            if (clientIdentifier != clientDto.ClientIdentifier)
            {
                result.Status = ResultStatus.Unauthorized;
                return result;
            }

            //then check the database if there is a row with given information
            var clientIdResult = await _licenceOperation.GetClientByClientIdentifier(clientIdentifier);
            if (!clientIdResult.IsSuccess)
            {
                //something went wrong
                return result;
            }
            clientDto.Id = clientIdResult.Data;
            // then get the client active licences
            var activeLicencesResult = await _licenceOperation.GetActiveLicence(clientDto.Id);
           
            return activeLicencesResult;
        }
    }
}
