using System.Linq;
using System.Web.Http;
using Dinazor.Core.Common.Attributes;
using Dinazor.Core.Database.Context;
using Dinazor.Module.NotificationManagement.Hub;
using Dinazor.Module.NotificationManagement.Mail;
using log4net;

namespace DinazorService.Controllers
{
    public class ValuesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        [ActionName("Default")]
        public IHttpActionResult Get()
        {
            
         /*   DinazorEventHub eventHub = new DinazorEventHub();
            eventHub.PublishMessage("topic","test");*/

            MailManager mailManager = new MailManager();
            mailManager.SendMail();

            return Ok("ok");
        }
    }
}