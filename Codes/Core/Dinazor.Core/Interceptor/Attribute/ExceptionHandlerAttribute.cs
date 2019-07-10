
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Filters;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Extensions;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Exceptions;
using log4net;

namespace Dinazor.Core.Interceptor.Attribute
{
    public class ExceptionHandlerAttribute: ExceptionFilterAttribute
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(HttpActionExecutedContext context)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            var exception = context.Exception as DinazorException;
            if (exception != null)
            {
                var exp = exception;
                _log.Error(exp);
                response.Content = new ObjectContent<DinazorResult>(exp.Result, new JsonMediaTypeFormatter(), "application/json");
                throw new HttpResponseException(response);
            }
            else
            {
                var exp = context.Exception;
                _log.Error(exp);
                response.Content = new ObjectContent<DinazorResult>(new DinazorResult()
                {
                    Status = ResultStatus.UnknownError,
                    Message = exp.GetAllMessages()
                }, new JsonMediaTypeFormatter(), "application/json");
            }

            context.Response = response;
            throw new HttpResponseException(response);
        }
    }
}
