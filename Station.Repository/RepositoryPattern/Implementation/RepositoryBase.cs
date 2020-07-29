using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Station.Core;
using Station.Entity.DB2AdminPattern;
using Station.Helper;
using Station.Repository.RepositoryPattern.SortApply;

namespace Station.Repository.RepositoryPattern.Implementation
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly IApplicationContext _applicationContext;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(DbContext dbContext, IApplicationContext applicationContext)
        {
            _dbContext = dbContext;
            _applicationContext = applicationContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual DbSet<T> CurrentDbSet => _dbSet;

        public virtual IQueryable<T> Get()
        {
            return CurrentDbSet;
        }

        #region 异步获取数据
        /// <summary>
        /// 主键异步获取单个entity
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public virtual async Task<T> GetSingleAsync(params object[] primaryKey)
        {
            return await CurrentDbSet.FindAsync(primaryKey);
        }

        /// <summary>
        /// 根据id异步获取单个entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetSingleAsync(string id)
        {
            return await _dbSet.FirstOrDefaultAsync(p => typeof(T).GetProperty(typeof(T).Name + "Id").GetValue(p).ToString().Equals(id));
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// 根据条件异步获取单个entity
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.SingleAsync(filter);
        }

        /// <summary>
        /// 异步获取entity所有数据
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAsync()
        {
            return await _dbSet.ToListAsync();
        }


        /// <summary>
        /// 根据Id的集合异步获取所有entity
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


        public async Task<IEnumerable<T>> GetAsync(IEnumerable<string> ids, Expression<Func<T, bool>> filter)
        {
            if (filter == null && ids == null)
                return await _dbSet.ToListAsync();
            IQueryable<T> result = null;
            if (ids != null)
                result = _dbSet.AsQueryable().Where(p => ids.Contains(typeof(T).GetProperty(typeof(T).Name + "Id").GetValue(p).ToString().TrimEnd()));
            if (filter != null)
                result ??= _dbSet.Where(filter);

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(IEnumerable<string> ids, Expression<Func<T, bool>> filter, string orderBy, Dictionary<string, PropertyMappingValue> propertyMapping)
        {
            if (filter == null && ids == null&& orderBy==null)
                return await _dbSet.ToListAsync();
            IQueryable<T> result = null;
            if (ids != null)
                result = _dbSet.AsQueryable().Where(p => ids.Contains(typeof(T).GetProperty(typeof(T).Name + "Id").GetValue(p).ToString().TrimEnd()));
            if (filter != null)
                result = result == null ? _dbSet.Where(filter) : result.Where(filter);
            if (orderBy != null)
                result = result == null? _dbSet.ApplySort(orderBy, propertyMapping):result.ApplySort(orderBy, propertyMapping);

            return await result.ToListAsync();
        }


        /// <summary>
        /// 根据条件异步获取entity集合
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
                return await _dbSet.ToListAsync();
            return await _dbSet.Where(filter).ToListAsync();
        }

        /// <summary>
        /// 根据条件异步分页查询集合
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter, Pagination pagination)
        {
            pagination.RecordCount = await CountAsync(filter);
            var result = filter != null ? Get().Where(filter) : Get();
            if (pagination.OrderBy != null || pagination.OrderByDescending != null)
            {
                result = pagination.OrderBy != null ? result.OrderBy(pagination.OrderBy) : result.OrderByDescending(pagination.OrderByDescending);

                if (pagination.ThenBy != null || pagination.ThenByDescending != null)
                {
                    result = pagination.ThenBy != null ? (result as IOrderedQueryable<T>).ThenBy(pagination.ThenBy) : (result as IOrderedQueryable<T>).ThenByDescending(pagination.ThenByDescending);
                }
            }
            return await result.Skip(pagination.PageIndex * pagination.PageSize).Take(pagination.PageSize).ToListAsync();
        }

        #endregion

        /// <summary>
        /// 资源条数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<T, bool>> filter)
        {
            if (filter != null)
            {
                return Get().Where(filter).Count();
            }
            return Get().Count();
        }

        /// <summary>
        /// 异步资源条数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            if (filter != null)
            {
                return await Get().Where(filter).CountAsync();
            }
            return await Get().CountAsync();
        }

        /// <summary>
        /// 添加entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            entity.GetType().GetProperty(typeof(T).Name + "Id")?.SetValue(entity, Guid.NewGuid().ToString());
            if (entity is EditorEntity editor)
            {
                if (_applicationContext.CurrentUser != null)
                {
                    editor.CreateDate = DateTime.Now.Date;
                    editor.UpdateDate = DateTime.Now.Date;
                    editor.Creator = _applicationContext.CurrentUser.UserName;
                    editor.Updater = _applicationContext.CurrentUser.UserName;
                }
            }

            _dbSet.Add(entity);
        }

        /// <summary>
        /// 添加entity集合
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Add(IList<T> entities)
        {
            //主键
            foreach (var entity in entities)
            {
                entity.GetType().GetProperty(typeof(T).Name + "Id")?.SetValue(entity, Guid.NewGuid().ToString());
                if (entity is EditorEntity editor)
                {
                    if (_applicationContext.CurrentUser != null)
                    {
                        editor.CreateDate = DateTime.Now.Date;
                        editor.UpdateDate = DateTime.Now.Date;
                        editor.Creator = _applicationContext.CurrentUser.UserName;
                        editor.Updater = _applicationContext.CurrentUser.UserName;
                    }
                }
            }
            _dbSet.AddRange(entities);
        }

        public virtual void Delete(string id)
        {
            var entity = _dbSet.FirstOrDefault(p => typeof(T).GetProperty(typeof(T).Name + "Id").GetValue(p).ToString().Equals(id));
            if (entity == null)
                throw new KeyNotFoundException($"{id}未找到数据");

            Delete(entity);
        }

        public virtual void Delete(IEnumerable<string> ids)
        {
            var entities = _dbSet.Where(p =>
                ids.Contains(typeof(T).GetProperty(typeof(T).Name + "Id").GetValue(p).ToString().TrimEnd())).ToList();
            if (!entities.Any())
            {
                _dbSet.RemoveRange(entities);
            }
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            //_dbContext.Entry(_dbSet).State = EntityState.Modified;
        }

        public virtual void Update(IEnumerable<T> entities)
        {

        }

        public virtual bool SaveChanges()
        {
            return _dbContext.SaveChanges() >= 0;
        }
    }
}