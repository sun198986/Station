using Station.Entity.DB2Admin;

namespace Station.Repository
{
    public interface IRepositoryBase
    {
        Db2AdminDbContext GetDbContext();
    }
}