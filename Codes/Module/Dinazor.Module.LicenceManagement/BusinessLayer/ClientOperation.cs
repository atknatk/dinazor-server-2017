using Dinazor.Core.Database.Entity;
using Dinazor.Core.Database.Entity.Crm;
using Dinazor.Core.Dto;
using Dinazor.Core.Dto.Crm;
using Dinazor.Core.GenericOperation.Abstracts;
using Dinazor.Core.Interfaces.Licence;

namespace Dinazor.Module.LicenceManagement.BusinessLayer
{
    public class ClientOperation : SoftDeletableRetrieveOperation<Client, ClientDto>, IClientOperation
    {
    }
}
