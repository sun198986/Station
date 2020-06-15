using IBM.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Station.Entity.DB2Admin
{
    public class Db2AdminDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseDb2("Database=U_PMASS;UID=MASSER1;PWD=8Om7.bN8v;Server=10.236.198.73", option =>
            {

            });
        }
        /// <summary>
        /// 维修注册
        /// </summary>
        public DbSet<Regist> Regists { get; set; }


    }
}