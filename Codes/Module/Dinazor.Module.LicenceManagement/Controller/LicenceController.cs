using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Crm;
using Dinazor.Core.GenericOperation.Controller;
using Dinazor.Core.Interfaces.Licence;
using Dinazor.Core.IoC; 

namespace Dinazor.Module.LicenceManagement.Controller
{
    public class LicenceController : DinazorController
    {
        private readonly ILicenceManager _licenceManager;

        public LicenceController()
        {
            _licenceManager = IocManager.Instance.Resolve<ILicenceManager>(); ;
        }

        [HttpPost]
        public async Task<DinazorResult<List<OrganizationLicenceDto>>> IsLicenceValid(ClientDto clientDto)
        {
            //clientDto.BiosVersion = "1.1.1";
            //clientDto.ClientIdentifier = "1.1.11uinan123";
            //clientDto.HddSerialNo = "1";
            //clientDto.Password = "123";
            //clientDto.Username = "uinan";
            return await _licenceManager.IsLicenceValid(clientDto);
        }
    }
}
