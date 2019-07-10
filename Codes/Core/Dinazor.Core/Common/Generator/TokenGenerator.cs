using System;

namespace Dinazor.Core.Common.Generator
{
    public class TokenGenerator
    {
        public static string GenerateUniqueId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
