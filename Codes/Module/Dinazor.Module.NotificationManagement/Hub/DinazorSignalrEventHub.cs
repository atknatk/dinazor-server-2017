
using System;
using Microsoft.AspNet.SignalR.Hubs;

namespace Dinazor.Module.NotificationManagement.Hub
{
    [HubName("DinazorSignalrEventHub")]
    public class DinazorSignalrEventHub : Microsoft.AspNet.SignalR.Hub
    {
        public void Subscribe(string topic)
        {
            Groups.Add(Context.ConnectionId, topic);
        }
        public void Unsubscribe(string topic)
        {
            Groups.Remove(Context.ConnectionId, topic);
        }
 
    }
}
