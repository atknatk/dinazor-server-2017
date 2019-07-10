using System;
using System.Web;

namespace Dinazor.Core.Identity
{
    public class IdentityHelper
    {
        private static readonly object LockObj = new Object();
        private static IdentityHelper _instance;

        public static IdentityHelper Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (LockObj)
                {
                    {
                        _instance = new IdentityHelper();
                    }
                }
                return _instance;
            }
        }

        public DinazorIdentity DinazorIdentity => ((DinazorIdentity)HttpContext.Current?.User?.Identity);
        public long CurrentUserId => DinazorIdentity?.TokenUser?.Id ?? -1;

        public string GetToken()
        {
            return DinazorIdentity?.TokenUser?.Token;
        }
    }
}
