using System;
using System.Collections.Generic;
using Station.Entity.DB2Admin;
using Station.Models.EmployeeDto;
using Station.Models.RegistDto;

namespace Station.SortApply.Helper
{
    public class PropertyMappingCollection
    {
        public IList<IPropertyMapping> PropertyMappings { get; set; }

        public PropertyMappingCollection()
        {
            PropertyMappings = new List<IPropertyMapping>
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
                new PropertyMapping<EmployeeDto, Employee>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
                {
                    {"EmployeeId",new PropertyMappingValue(new List<string>{"EmployeeId"})},
                    {"EmployeeName",new PropertyMappingValue(new List<string>{"EmployeeName"})},
                    {"RegistId",new PropertyMappingValue(new List<string>{"RegistId"})}
                })
            };
        }
    }
}