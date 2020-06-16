
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Station.Helper
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
}