using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Station.Helper.Extensions
{
    public class ArrayModelBinder: IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bingContext)
        {
            if (!bingContext.ModelMetadata.IsEnumerableType)
            {
                bingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var value = bingContext.ValueProvider.GetValue(bingContext.ModelName).ToString();
            if (string.IsNullOrWhiteSpace(value))
            {
                bingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            var elementType = bingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
            var converter = TypeDescriptor.GetConverter(elementType);
            var values = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x.Trim())).ToArray();


            var typedValues = Array.CreateInstance(elementType, values.Length);
            values.CopyTo(typedValues, 0);
            bingContext.Model = typedValues;

            bingContext.Result = ModelBindingResult.Success(bingContext.Model);
            return Task.CompletedTask;
        }
    }

    public class DtoModelBinder<T> : IModelBinder where T : new()
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();
            if (string.IsNullOrWhiteSpace(value))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            var values = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim()).ToArray();

            T t = new T();

            foreach (string val in values)
            {
                var valSplitArray = val.Split(new[] {":"}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()).ToArray();
                var valFirst = valSplitArray.FirstOrDefault();
                var valEnd = valSplitArray.LastOrDefault();
                if (valFirst == null || valEnd == null)
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                    return Task.CompletedTask;
                }

                try
                {
                    t.GetType().GetProperty(valFirst)?.SetValue(t, valEnd);
                }
                catch (Exception e)
                {
                    throw new Exception($"参数{valFirst}异常:{e.Message}");
                }
            }

            bindingContext.Model = t;
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}