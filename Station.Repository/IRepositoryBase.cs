using Station.EFCore.IbmDb;
using Station.Entity.DB2Admin;

namespace Station.Repository
{
    public interface IRepositoryBase
    {
        IbmDbContext GetDbContext();
    }
}