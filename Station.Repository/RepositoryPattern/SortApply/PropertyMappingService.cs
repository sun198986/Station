using System;
using System.Collections.Generic;
using System.Linq;
using Station.Entity.DB2Admin;
using Station.Models.EmployeeDto;
using Station.Models.RegistDto;

namespace Station.Repository.RepositoryPattern.SortApply
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private readonly Dictionary<string, PropertyMappingValue> _registPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"RegistId",new PropertyMappingValue(new List<string>{"RegistId"})},
                {"RegistDate",new PropertyMappingValue(new List<string>{"RegistDate"})},
                {"MaintainNumber",new PropertyMappingValue(new List<string>{"MaintainNumber"})},
                {"CustomName",new PropertyMappingValue(new List<string>{"CustomName"})},
                {"Address",new PropertyMappingValue(new List<string>{"Address"})},
                {"Linkman",new PropertyMappingValue(new List<string>{"Linkman"})},
                {"Phone",new PropertyMappingValue(new List<string>{"Phone"})},
                {"Fax",new PropertyMappingValue(new List<string>{"Fax"})}
            };

        private readonly Dictionary<string, PropertyMappingValue> _employeePropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"EmployeeId",new PropertyMappingValue(new List<string>{"EmployeeId"})},
                {"EmployeeName",new PropertyMappingValue(new List<string>{"EmployeeName"})},
                {"RegistId",new PropertyMappingValue(new List<string>{"RegistId"})}
            };

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();


        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<RegistDto, Regist>(_registPropertyMapping));
            _propertyMappings.Add(new PropertyMapping<EmployeeDto,Entity.DB2Admin.Employee>(_employeePropertyMapping));
        }
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();
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