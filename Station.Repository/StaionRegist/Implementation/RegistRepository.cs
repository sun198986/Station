using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Station.Entity.DB2Admin;

namespace Station.Repository.StaionRegist.Implementation
{
    public class RegistRepository:IRegistRepository
    {
        private readonly Db2AdminDbContext _db2AdminDbContext;

        public RegistRepository(Db2AdminDbContext db2AdminDbContext)
        {
            _db2AdminDbContext = db2AdminDbContext;
        }

        public async Task<IEnumerable<Regist>> GetRegistsAsync()
        {
            var list = await _db2AdminDbContext.Regists.ToListAsync();
            return list;
        }

        public async Task<IEnumerable<Regist>> GetRegistsAsync(IEnumerable<string> registIds)
        {
            if (registIds == null)
            {
                throw new ArgumentNullException(nameof(registIds));
            }
            return await _db2AdminDbContext.Regists
                .Where(x => registIds.Contains(x.RegistId))
                .OrderBy(x => x.RegistId)
                .ToListAsync();
        }

        public void AddRegist(Regist regist)
        {
            if (regist == null)
            {
                throw new ArgumentNullException(nameof(regist));
            }

            regist.RegistId = Guid.NewGuid().ToString();

            _db2AdminDbContext.Regists.Add(regist);
        }


        public async Task<bool> SaveAsync()
        {
            return await _db2AdminDbContext.SaveChangesAsync() >= 0;
        }
    }
}