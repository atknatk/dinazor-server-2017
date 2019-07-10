using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinazor.Core.Database.Paging
{
    internal class PagingGenericTypeHelper :    IPagingTypeBuilder,
                                                IPagingConstructorBuilder,
                                                IPagingInstanceBuilder
    {
        #region Fields

        private readonly Type _type;
        private object[] _constructorArguments;
        private Type[] _typeArguments;

        #endregion

        #region Constructors and Destructors

        private PagingGenericTypeHelper(Type type)
        {
            _type = type;
        }

        #endregion

        #region Public Methods and Operators


        public IPagingInstanceBuilder WithConstructorArguments(object[] constructorArguments)
        {
            _constructorArguments = constructorArguments;
            return this;
        }

        public object CreateInstance()
        {
            Type type = _type.MakeGenericType(_typeArguments);
            return Activator.CreateInstance(type, _constructorArguments);
        }

   
        public IPagingConstructorBuilder WithTypeArguments(Type[] typeArguments)
        {
            _typeArguments = typeArguments;
            return this;
        }
        
        public static IPagingTypeBuilder Create(Type type)
        {
            return new PagingGenericTypeHelper(type);
        }

        #endregion
    }
}
