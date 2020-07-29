using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Station.SortApply.Helper
{
    [ServiceDescriptor(typeof(IPropertyMappingService), ServiceLifetime.Transient)]
    public class PropertyMappingService : IPropertyMappingService
    {
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>(IList<IPropertyMapping> propertyMappings)
        {
            var matchingMapping = propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();
            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().MappingDictionary;
            }
            throw new Exception($"无法找到唯一的依赖关系:{typeof(TSource)},{typeof(TDestination)}");
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(IList<IPropertyMapping> propertyMappings, string fields)
        {
            var propertyMapping = GetPropertyMapping <TSource, TDestination> (propertyMappings);
            if (string.IsNullOrWhiteSpace(fields))
                return true;

            var filedAfterSplit = fields.Split(",");
            foreach (var field in filedAfterSplit)
            {
                var trimmedField = field.Trim();
                var indexOfFirstSpace = trimmedField.IndexOf(" ", StringComparison.Ordinal);
                var propertyName = indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace);

                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }

            return true;
        }

    }
}