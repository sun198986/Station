using System.Collections.Generic;
using System.Threading.Tasks;
using Station.EFCore.IbmDb;
using Station.Entity.DB2Admin;
using Station.Repository.Employee;

namespace Station.Repository
{
    public interface IRepositoryBase<T>
    {

        Task<IEnumerable<T>> GetAsync();

        Task<IEnumerable<T>> GetAsync(IEnumerable<string> ids);

        Task<T> GetAsync(string id);

        void Add(T entity);

        void Add(IEnumerable<T> entities);

        void Delete(string id);

        void Delete(T entity);

        void Delete(IEnumerable<T> entities);

        void Update(T entity);

        bool SaveChanges();
    }
}