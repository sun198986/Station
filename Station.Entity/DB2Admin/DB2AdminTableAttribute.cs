using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace Station.Entity.DB2Admin
{
    public class DB2AdminTableAttribute : TableAttribute
    {
        public DB2AdminTableAttribute(string name):base(name)
        {
            base.Schema = "DB2Admin";
        }
    }
}
