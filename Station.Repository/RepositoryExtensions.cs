using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Station.Repository
{
    public static class RepositoryExtensions
    {

        public static async Task<IEnumerable<TU>> GetAsync<T, TU>(this T t) where T : IRepositoryBase where TU : class
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            return await t.GetDbContext().Set<TU>().ToListAsync();
        }

        public static async Task<TU> GetAsync<T,TU>(this T t,string id) where T : IRepositoryBase where TU : class
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            return await t.GetDbContext().Set<TU>().FindAsync(id);
        }

        public static async Task<IEnumerable<TU>> GetAsync<T,TU>(this T t, IEnumerable<string> ids) where T : IRepositoryBase where TU : class
        {
            if (t == null) throw new ArgumentNullException(nameof(t));

            return await t.GetDbContext().Set<TU>()
                .Where(p => ids.Contains(typeof(TU).GetProperty(typeof(TU).Name + "Id").GetValue(p))).ToListAsync();
        }

        public static  IEnumerable<TU> GetByIds<T,TU>(this T t, string primaryKeyName, IEnumerable<string> ids) where T : IRepositoryBase where TU : class
        {
            if (t == null) throw new ArgumentNullException(nameof(t));

            return t.GetDbContext().Set<TU>()
                .Where(p => ids.Contains(typeof(TU).GetProperty(primaryKeyName).GetValue(p))).ToList();
        }

        public static void Add<T, TU>(this T t, TU entity) where T : IRepositoryBase where TU : class
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            t.GetDbContext().Set<TU>().Add(entity);
        }

        public static void Add<T, TU>(this T t, IEnumerable<TU> entities) where T : IRepositoryBase where TU : class
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            t.GetDbContext().Set<TU>().AddRange(entities);
        }

        public static void Delete<T, TU>(this T t, TU entity) where T : IRepositoryBase where TU : class
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            t.GetDbContext().Set<TU>().Remove(entity);
        }

        public static void Delete<T, TU>(this T t, IEnumerable<TU> entities) where T : IRepositoryBase where TU : class
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            t.GetDbContext().Set<TU>().RemoveRange(entities);
        }

        public static void Update<T, TU>(this T t, TU entity) where T : IRepositoryBase where TU : class
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            if (entity == null) throw new ArgumentNullException(nameof(entity));
        }

        public static bool SaveChanges<T>(this T t) where T : IRepositoryBase
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            return t.GetDbContext().SaveChanges() >= 0;
        }

    }
}