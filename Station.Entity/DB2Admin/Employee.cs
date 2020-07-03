using System;
using System.Collections.Generic;
using System.Text;

namespace Station.Entity.DB2Admin
{
    [DB2AdminTable("Employee")]
    public class Employee
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string RegistId { get; set; }
    }
}
