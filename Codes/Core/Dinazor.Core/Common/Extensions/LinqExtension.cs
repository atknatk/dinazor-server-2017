using Dinazor.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Dinazor.Core.Common.Extensions
{
    public static class LinqExtension
    {
        public static IQueryable<TSource> Paging<TSource>(this IQueryable<TSource> source, PagingRequest paging)
        {
            return source.OrderBy(paging).Skip(paging.start).Take(paging.length);
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, PagingRequest paging)
        {
            if (!paging.order.Any()) return (IOrderedQueryable<T>)source;
            return ApplyOrder<T>(source, paging.columns[paging.order[0].column].data, paging.order[0].dir == "asc" ? "OrderBy" : "OrderByDescending");
        }

        static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
    }
}
