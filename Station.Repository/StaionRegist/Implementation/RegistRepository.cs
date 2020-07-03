using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Station.EFCore.IbmDb;
using Station.Entity.DB2Admin;

namespace Station.Repository.StaionRegist.Implementation
{
    public class RegistRepository:IRegistRepository
    {
        private IbmDbContext _context;

        public RegistRepository(IbmDbContext context)
        {
            _context = context;
        }
        public IbmDbContext GetDbContext() => _context;

        public async Task<IEnumerable<Regist>> GetRegistsAsync()
        {
            var list = await _context.Regists.ToListAsync();
            return list;
        }

        public async Task<Regist> GetRegistsAsync(string registId)
        {
            if (string.IsNullOrWhiteSpace(registId))
            {
                throw new ArgumentNullException(nameof(registId));
            }

            return await _context.Regists
                .FirstOrDefaultAsync(x => x.RegistId.Equals(registId));
        }

        public async Task<IEnumerable<Regist>> GetRegistsAsync(IEnumerable<string> registIds)
        {
            if (registIds == null)
            {
                throw new ArgumentNullException(nameof(registIds));
            }
            return await _context.Regists
                .Where(x => registIds.Contains(x.RegistId))
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

        public void AddRegist(IEnumerable<Regist> regists)
        {
            if (regists == null)
            {
                throw new ArgumentNullException(nameof(regists));
            }

            foreach (var regist in regists)
            {
                regist.RegistId = Guid.NewGuid().ToString();
            }
            _context.Regists.AddRange(regists);
        }


        public void DeleteRegist(Regist regist)
        {
            if (regist == null)
            {
                throw new ArgumentNullException(nameof(regist));
            }

            _context.Regists.Remove(regist);
        }

        public void DeleteRegist(IEnumerable<Regist> regists)
        {
            if (regists == null)
            {
                throw new ArgumentNullException(nameof(regists));
            }
            _context.Regists.RemoveRange(regists);
        }

        public void UpdateRegist(Regist regist)
        {
            //throw new NotImplementedException();
        }

        public async Task<bool> RegistExistsAsync(string registId)
        {
            if (string.IsNullOrWhiteSpace(registId))
            {
                throw new ArgumentNullException(nameof(registId));
            }
            return await _context.Regists.AnyAsync(x => x.RegistId == registId);
        }


        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        //public bool SaveChange()
        //{
        //    return _context.SaveChanges() >= 0;
        //}
    }
}