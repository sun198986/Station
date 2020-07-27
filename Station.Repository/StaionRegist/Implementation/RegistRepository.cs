using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Station.EFCore.IbmDb;
using Station.Entity.DB2Admin;
using Station.Repository.Employee;
using Station.Repository.RepositoryPattern;
using Station.Repository.RepositoryPattern.Implementation;

namespace Station.Repository.StaionRegist.Implementation
{
    [ServiceDescriptor(typeof(IRegistRepository), ServiceLifetime.Transient)]
    public class RegistRepository:RepositoryBase<Regist>,IRegistRepository
    {
        private IbmDbContext _context;

        public RegistRepository(IbmDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<Regist> GetSingleRegistAndEmployeeAsync(string registId)
        {
            var regist = await _context.Regists.Include(x => x.Employees).Where(p => p.RegistId == registId)
                .FirstOrDefaultAsync();
            return regist;
        }

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
            return await _context.Regists.Where(x => x.RegistId.Trim() == registId).FirstOrDefaultAsync()!=null;
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