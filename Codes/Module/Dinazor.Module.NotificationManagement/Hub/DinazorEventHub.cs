
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Dinazor.Module.NotificationManagement.Hub
{
    [HubName("DinazorEventHub")]
    public class DinazorEventHub
    {

        public void PublishMessage(string topic,string message)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<DinazorSignalrEventHub>();
            ((IClientProxy)hub.Clients.All).Invoke(topic, message).Wait();
        }

    }
}
