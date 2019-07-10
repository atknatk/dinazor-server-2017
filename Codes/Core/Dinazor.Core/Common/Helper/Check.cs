using System;
using Dinazor.Core.Common.Attributes;

namespace Dinazor.Core.Common.Helper
{
    public static class Check
    {
        [Annotations.ContractAnnotationAttribute("value:null => halt")]
        public static T NotNull<T>(T value, [Annotations.InvokerParameterNameAttribute] [Annotations.NotNullAttribute] string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }
    }
}
