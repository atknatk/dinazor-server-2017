
using Dinazor.Core.Identity;

namespace Dinazor.Core.Common.Helper
{
    public class CustomAttributeHelper
    {
        public static void AddOrUpdateCustomAttribute(string key, object value)
        {
            if (IdentityHelper.Instance.DinazorIdentity != null && IdentityHelper.Instance.DinazorIdentity.TokenUser != null && IdentityHelper.Instance.DinazorIdentity.TokenUser.CustomAttributes != null)
            {
                IdentityHelper.Instance.DinazorIdentity.TokenUser.CustomAttributes[key] = value;
            }
        }

        public static object GetCustomAttribute(string key)
        {
            if (IdentityHelper.Instance.DinazorIdentity != null && IdentityHelper.Instance.DinazorIdentity.TokenUser != null && IdentityHelper.Instance.DinazorIdentity.TokenUser.CustomAttributes != null)
            {
                if (IdentityHelper.Instance.DinazorIdentity.TokenUser.CustomAttributes.ContainsKey(key))
                {
                    return IdentityHelper.Instance.DinazorIdentity.TokenUser.CustomAttributes[key];
                }
            }
            return null;
        }
    }
}
