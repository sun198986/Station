using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Station.EFCore.IbmDb;

namespace Station.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        /// <summary>
        /// 获取entity所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// 根据Id的集合获取所有entity
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAsync(IEnumerable<string> ids)
        {
            if (!ids.Any())
            {
                throw new ArgumentNullException(nameof(ids));
            }
            return await _dbSet
                 .Where(p => ids.Contains(typeof(T).GetProperty(typeof(T).Name + "Id").GetValue(p).ToString().TrimEnd())).ToListAsync();
        }


        /// <summary>
        /// 根据id获取entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(string id)
        {
            return await _dbSet.FirstOrDefaultAsync(p=> typeof(T).GetProperty(typeof(T).Name + "Id").GetValue(p).ToString().Equals(id));
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// 添加entity
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// 添加entity集合
        /// </summary>
        /// <param name="entities"></param>
        public void Add(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Delete(string id)
        {
            var entity = _dbSet.FirstOrDefault(p => typeof(T).GetProperty(typeof(T).Name + "Id").GetValue(p).ToString().Equals(id));
            if(entity==null)
                throw new KeyNotFoundException($"{id}未找到数据");

            Delete(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            //_dbContext.Entry(_dbSet).State = EntityState.Modified;
        }

        public bool SaveChanges()
        {
            return _dbContext.SaveChanges() >= 0;
        }
    }
}