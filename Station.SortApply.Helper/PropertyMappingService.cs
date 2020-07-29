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
        private readonly PropertyMappingCollection _propertyMappingCollection;

        public PropertyMappingService(PropertyMappingCollection propertyMappingCollection)
        {
            _propertyMappingCollection = propertyMappingCollection;
        }
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappingCollection.PropertyMappings.OfType<PropertyMapping<TSource, TDestination>>();
            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().MappingDictionary;
            }
            throw new Exception($"无法找到唯一的依赖关系:{typeof(TSource)},{typeof(TDestination)}");
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping <TSource, TDestination> ();
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