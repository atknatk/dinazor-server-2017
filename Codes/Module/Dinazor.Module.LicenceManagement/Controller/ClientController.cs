using Dinazor.Core.Dto;
using Dinazor.Core.Dto.Crm;
using Dinazor.Core.GenericOperation.Controller;
using Dinazor.Core.Interfaces.Licence;

namespace Dinazor.Module.LicenceManagement.Controller
{
    public class ClientController : DinazorController<IClientManager, ClientDto>,IDinazorCrudController<ClientDto>
    {

    }
}
