using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Station.Repository.RepositoryPattern.SortApply;

namespace Station.Repository.RepositoryPattern
{
    public interface IRepositoryBase<T>
    {
        Task<T> GetSingleAsync(params object[] primaryKey);

        Task<T> GetSingleAsync(string id);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAsync();

        Task<IEnumerable<T>> GetAsync(IEnumerable<string> ids);

        Task<IEnumerable<T>> GetAsync(IEnumerable<string> ids, Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAsync(IEnumerable<string> ids, Expression<Func<T, bool>> filter, string orderBy, Dictionary<string, PropertyMappingValue> propertyMapping);

        Task<IEnumerable<T>> GetAsync(IEnumerable<string> ids, Sort sort);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter,Sort sort);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter, Pagination pagination);

        int Count(Expression<Func<T, bool>> filter);

        Task<int> CountAsync(Expression<Func<T, bool>> filter);

        void Add(T entity);

        void Add(IList<T> entities);

        void Delete(string id);

        void Delete(T entity);

        void Delete(IEnumerable<string> ids);

        void Delete(IEnumerable<T> entities);

        void Update(T entity);

        void Update(IEnumerable<T> entities);

        bool SaveChanges();
    }
}