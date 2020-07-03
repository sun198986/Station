using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Station.Entity.DB2Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace Station.EFCore.IbmDb
{
    public class IbmDbContext : DbContext
    {
        public IbmDbContext(DbContextOptions<IbmDbContext> options):base(options)
        {

        }

        /// <summary>
        /// 维修注册
        /// </summary>
        public DbSet<Regist> Regists { get; set; }

    }
}
