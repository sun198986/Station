using System;
using System.Threading;
using System.Threading.Tasks;
using IBM.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace Station.Entity.DB2Admin
{
    public class Db2AdminDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseDb2("Database=U_PMASS;UID=MASSER1;PWD=8Om7.bN8v;Server=10.236.198.73", option =>
            {

            }).UseLoggerFactory(ConsoleLoggerFactory);
        }
        public Db2AdminDbContext(DbContextOptions<Db2AdminDbContext> options) : base(options)
        {
            
        }

        /// <summary>
        /// 维修注册
        /// </summary>
        public DbSet<Regist> Regists { get; set; }

        public static readonly ILoggerFactory ConsoleLoggerFactory =
            LoggerFactory.Create(builder =>
            {
                builder.AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information).AddConsole();
            });
    }
}