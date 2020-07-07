using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Station.Entity.DB2Admin
{
    [Db2AdminTable("Employee")]
    public class Employee
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key, Column("EMPLOYEEID")]
        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string RegistId { get; set; }
    }
}
