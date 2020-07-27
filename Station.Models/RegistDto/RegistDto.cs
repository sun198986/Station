using System;
using System.Collections.Generic;

namespace Station.Models.RegistDto
{
    public class RegistDto
    {
        public string RegistId { set; get; }

        public DateTime RegistDate { set; get; }

        public string MaintainNumber { set; get; }

        public string CustomName { get; set; }

        public string Address { get; set; }

        public string Linkman { get; set; }

        public string TelPhone { get; set; }

        public string Fax { get; set; }

        public IEnumerable<EmployeeDto.EmployeeDto> Employees { get; set; }
    }
}