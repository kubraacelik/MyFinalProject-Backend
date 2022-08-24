using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception  //Aspect : Metodun başında, sonunda hata verdiğinde çalışacak yapı
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil.");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)  //Doğrulama metodun başında yapılacağı için bunu kullandım
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //çalışma anında instance oluşturmak istersen bu kodu yaz.(new'ler yani)
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //productvalidator'ın base tipinin generic argumantlarının 0.'nın tipini yakala 
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); //metodun argumanlarını gez. eğer ordaki bir tip product türündeyse
            foreach (var entity in entities)                                           //onları validate et
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
