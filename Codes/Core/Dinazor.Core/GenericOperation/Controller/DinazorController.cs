using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Interfaces;
using Dinazor.Core.GenericOperation.Interfaces;
using Dinazor.Core.IoC;
using Dinazor.Core.Model;
using log4net;

namespace Dinazor.Core.GenericOperation.Controller
{
    [Route("api/[controller]")]
    public abstract class DinazorController : ApiController
    {
    }

    public abstract class DinazorController<TManager, TDto> : DinazorController
        where TDto : IDto
        where TManager : IDinazorManager<TDto>
    {

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected readonly IDinazorManager<TDto> Manager;
        private readonly DinazorResult _unSupportedOperation = new DinazorResult()
        {
            Status = ResultStatus.UnsupportedOperation
        };

        protected DinazorController()
        {
            Manager = IocManager.Instance.Resolve<TManager>();
        }

        [HttpPost]
        [ActionName("Default")]
        public async Task<DinazorResult> Save(string token, [FromBody] TDto dto)
        {
            if (this is ISaveController<TDto>)
            {
                return await Manager.Save(dto);
            }
            return _unSupportedOperation;
        }

        [HttpPost]
        [ActionName("List")]
        public async Task<DinazorResult> SaveList(string token, [FromBody]List<TDto> dtoList)
        {
            if (this is ISaveListController<TDto>)
            {
                return await Manager.SaveList(dtoList);
            }
            return _unSupportedOperation;

        }

        [HttpDelete]
        public async Task<DinazorResult> Delete(string token, long id)
        {
            if (this is IDeleteController)
            {
                return await Manager.Delete(id);
            }
            return _unSupportedOperation;
        }

        [HttpPut]
        public async Task<DinazorResult> Update(string token, long id, [FromBody] TDto dto)
        {
            if (this is IUpdateController<TDto>)
            {
                return await Manager.Update(dto);
            }
            return _unSupportedOperation;
        }

        [HttpGet]
        public async Task<DinazorResult<TDto>> GetById(string token, long id)
        {
            if (this is IRetrieveController<TDto>)
            {
                return await Manager.GetById(id);
            }
            return new DinazorResult<TDto>() { Status = _unSupportedOperation.Status };
        }

        [HttpGet]
        [ActionName("Default")]
        public async Task<DinazorResult<List<TDto>>> GetAll(string token)
        {
            Log.Error("Hello");
            if (this is IRetrieveController<TDto>)
            {
                return await Manager.GetAll();
            }
            return new DinazorResult<List<TDto>>() { Status = _unSupportedOperation.Status };
        }

        [HttpGet]
        [ActionName("Join")]
        public async Task<DinazorResult<TDto>> GetAllWithJoin(string token,[FromUri]long id,string join)
        {
            if (this is ISelectWithJoinController<TDto>)
            {
                return await Manager.GetAllWithJoin(id);
            }
            return new DinazorResult<TDto>() { Status = _unSupportedOperation.Status };
        }

        [HttpPost]
        [ActionName("Paging")]
        public async Task<DinazorResult<PagingReply<TDto>>> Paging(string token,[FromBody]PagingRequest pagingRequest)
        {
            if (this is IPagingController<TDto>)
            {
                return await Manager.Paging(pagingRequest);
            }
            return new DinazorResult<PagingReply<TDto>>() { Status = _unSupportedOperation.Status };
        }
    }
}
