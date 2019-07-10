using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinazor.Core.Common.Attributes
{
    public class PagingAttribute : Attribute
    {
        public void PagingAttributee<T>(System.Func<T,bool> func)
        {

        }

    }
}
