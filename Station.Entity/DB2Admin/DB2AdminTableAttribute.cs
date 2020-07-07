using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace Station.Entity.DB2Admin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Db2AdminTableAttribute : TableAttribute
    {
        public Db2AdminTableAttribute(string name):base(name)
        {
            base.Schema = "DB2Admin";
        }
    }
}
