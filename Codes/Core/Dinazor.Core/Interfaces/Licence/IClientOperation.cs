using Dinazor.Core.Dto;
using Dinazor.Core.Dto.Crm;
using Dinazor.Core.GenericOperation.Interfaces;

namespace Dinazor.Core.Interfaces.Licence
{
    public interface IClientOperation : ISoftDeletableRetrieveOperation<Database.Entity.Crm.Client, ClientDto>
    {
    }
}
