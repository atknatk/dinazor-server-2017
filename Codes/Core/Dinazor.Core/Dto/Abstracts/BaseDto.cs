using System; 
using System.Linq;  
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations; 
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Interfaces;

namespace Dinazor.Core.Dto.Abstracts
{
    public abstract class BaseDto<TEntity, TDto> : IDto
        where TDto : BaseDto<TEntity, TDto>
    {
        public long Id { get; set; }

        public DinazorResult IsValid()
        {
            var result = new DinazorResult();
            var properties = GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.Name == "IsExist" || propertyInfo.Name == "Select") continue;
                var attrs = propertyInfo.GetCustomAttributes(true);
                var value = propertyInfo.GetValue(this);
                var validAttr = attrs.OfType<ValidationAttribute>().FirstOrDefault(valid => !valid.IsValid(value));
                if (validAttr == null) continue;
                result.Message = string.IsNullOrEmpty(validAttr.ErrorMessageResourceName) ? (string.IsNullOrEmpty(validAttr.ErrorMessage) ? validAttr.FormatErrorMessage(propertyInfo.Name) : validAttr.ErrorMessage) : validAttr.ErrorMessageResourceName;
                result.Status = ResultStatus.InValidParamater;
                return result;
            }
            return result.Success();
        }

        public abstract TEntity ToEntity();

        public abstract Expression<Func<TEntity, TDto>> Select();

        public TDto ToDto(TEntity entityModel)
        {
            return Select().Compile().Invoke(entityModel);
        }

        protected static string Name<T>(Expression<Func<T>> propertyLambda)
        {
            MemberExpression me = propertyLambda.Body as MemberExpression;
            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            string result = string.Empty;
            do
            {
                result = me.Member.Name + "." + result;
                me = me.Expression as MemberExpression;
            } while (me != null);

            result = result.Remove(result.Length - 1);
            return result;
        }

        //public virtual Expression<Func<TEntity, bool>> IsDeletable()
        //{
        //    return (l =>true);
        //}
    }

    public abstract class UniqueDto<TEntity, TDto> : BaseDto<TEntity, TDto>
            where TDto : BaseDto<TEntity, TDto>
    {
        public abstract Expression<Func<TEntity, bool>> IsExist();
    }
}
