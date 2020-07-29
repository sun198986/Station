using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Station.Entity.DB2Admin;
using Station.Models.EmployeeDto;
using Station.Models.RegistDto;
using Station.SortApply.Helper;

namespace Station.Repository.RepositoryPattern
{
    public static class PropertyMappingConfig
    {
        public static IList<IPropertyMapping> PropertyMappings = new List<IPropertyMapping>
        {
            new PropertyMapping<RegistDto, Regist>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"RegistId",new PropertyMappingValue(new List<string>{"RegistId"})},
                {"RegistDate",new PropertyMappingValue(new List<string>{"RegistDate"})},
                {"MaintainNumber",new PropertyMappingValue(new List<string>{"MaintainNumber"})},
                {"CustomName",new PropertyMappingValue(new List<string>{"CustomName"})},
                {"Address",new PropertyMappingValue(new List<string>{"Address"})},
                {"Linkman",new PropertyMappingValue(new List<string>{"Linkman"})},
                {"TelPhone",new PropertyMappingValue(new List<string>{"Phone"})},
                {"Fax",new PropertyMappingValue(new List<string>{"Fax"})}
            }),
            new PropertyMapping<EmployeeDto, Entity.DB2Admin.Employee>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"EmployeeId",new PropertyMappingValue(new List<string>{"EmployeeId"})},
                {"EmployeeName",new PropertyMappingValue(new List<string>{"EmployeeName"})},
                {"RegistId",new PropertyMappingValue(new List<string>{"RegistId"})}
            })
        };

        public static void InitPropertyMappingConfig(this IServiceCollection service)
        {
            IList<IPropertyMapping> property = PropertyMappings;
        }
    }
}