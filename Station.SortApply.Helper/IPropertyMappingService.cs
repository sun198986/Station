using System.Collections.Generic;

namespace Station.SortApply.Helper
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>(IList<IPropertyMapping> propertyMappings);
        bool ValidMappingExistsFor<TSource, TDestination>(IList<IPropertyMapping> propertyMappings,string fields);
    }
}