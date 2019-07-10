
using System.Collections.Generic;
using Dinazor.Core.Model;

namespace Dinazor.Core.Observer
{
    public static class LoginSubscriber
    {

        private static List<ILoginNotify> SubscriberList { get; set; } = new List<ILoginNotify>();

        public static void Subscribe(ILoginNotify loginNotify)
        {
            SubscriberList.Add(loginNotify);
        }

        public static void UnSubscribe(ILoginNotify loginNotify)
        {
            SubscriberList.Remove(loginNotify);
        }

        public static void Broadcast(TokenUser tokenUser)
        {
            foreach (var item in SubscriberList)
            {
                item.Notify(tokenUser);
            }
        }


    }
}
