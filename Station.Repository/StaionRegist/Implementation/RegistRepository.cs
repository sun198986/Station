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
        private readonly Db2AdminDbContext _context;

        public RegistRepository(Db2AdminDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Regist>> GetRegistsAsync()
        {
            var list = await _context.Regists.ToListAsync();
            return list;
        }

        public async Task<IEnumerable<Regist>> GetRegistsAsync(IEnumerable<string> registIds)
        {
            if (registIds == null)
            {
                throw new ArgumentNullException(nameof(registIds));
            }
            return await _context.Regists
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
            _context.Regists.Add(regist);
            //_context.SaveChanges();
        }


        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public bool SaveChange()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}