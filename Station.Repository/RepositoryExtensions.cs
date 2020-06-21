using System;
using System.Runtime.InteropServices.ComTypes;

namespace Station.Repository
{
    public static class RepositoryExtensions
    {
        public static void Add<T, TU>(this T t, TU entity) where T : IRepositoryBase where TU : class
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            t.GetDbContext().Set<TU>().Add(entity);
        }

        public static bool SaveChange<T>(this T t) where T : IRepositoryBase
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            return t.GetDbContext().SaveChanges() >= 0;
        }
    }
}