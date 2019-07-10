using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.Dto.Crm;
using Dinazor.Core.Interfaces.Licence;
using Dinazor.Core.Model;

namespace Dinazor.Module.LicenceManagement.Manager
{
    public class ClientManager : IClientManager
    {
        private readonly IClientOperation _clientOperation;

        public ClientManager(IClientOperation clientOperation)
        {
            _clientOperation = clientOperation;
        }

        public async Task<DinazorResult> Save(ClientDto clientDto)
        {
            return await _clientOperation.Save(clientDto);
        }

        public Task<DinazorResult> SaveList(List<ClientDto> tList)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DinazorResult> Delete(long id)
        {
            return await _clientOperation.Delete(id);
        }

        public async Task<DinazorResult> Update(ClientDto clientDto)
        {
            return await _clientOperation.Update(clientDto);
        }

        public async Task<DinazorResult<ClientDto>> GetById(long id)
        {
            return await _clientOperation.GetById(id);
        }

        public async Task<DinazorResult<List<ClientDto>>> GetAll()
        {
            return await _clientOperation.GetAll();
        }

        public Task<DinazorResult<ClientDto>> GetAllWithJoin(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<DinazorResult<PagingReply<ClientDto>>> Paging(PagingRequest pagingRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
