using System;
using System.Data.Entity.Core.Objects;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using log4net;

namespace Dinazor.Core.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter, IDisposable
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool AllowMultiple => throw new NotImplementedException();

        //TODO log
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(DinazorAuthorizationException))
            {
                var result = new DinazorResult();
                result.Message = context.Exception.Message;
                result.Status = ResultStatus.Unauthorized;
              /*  context.Result = new ObjectResult(result)
                {
                    StatusCode = 400,
                    DeclaredType = typeof(DinazorResult)
                };*/
            }
            _log.Error("Exception occured ", context.Exception);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
