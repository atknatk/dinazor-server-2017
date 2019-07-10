
using System;
using System.Net.Mail;
using System.Text;

namespace Dinazor.Module.NotificationManagement.Mail
{
    public class MailManager
    {
        public void SendMail()
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("umurinan@gmail.com", "***");

                MailMessage mm = new MailMessage("donotreply@domain.com", "umurinan@gmail.com", "test", "test");
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
            }
            catch (Exception e)
            {
                 
            }
        }

    }
}
