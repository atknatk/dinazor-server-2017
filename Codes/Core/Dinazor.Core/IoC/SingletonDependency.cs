using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinazor.Core.IoC
{
    public class SingletonDependency<T>
    {
        public static T Instance
        {
            get
            {
                return LazyInstance.Value;
            }
        }
        private static readonly Lazy<T> LazyInstance;

        static SingletonDependency()
        {
            LazyInstance = new Lazy<T>(() => IocManager.Instance.Resolve<T>());
        }
    }
}

