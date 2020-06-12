using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Station.Entities.DB2Admin;

namespace Station.Repositories.StaionRegist.Implementation
{
    public class RegistRepository:IRegistRepository
    {
        private readonly Db2AdminDbContext _db2AdminDbContext;

        public RegistRepository(Db2AdminDbContext db2AdminDbContext)
        {
            _db2AdminDbContext = db2AdminDbContext;
        }

        public async Task<IList<Regist>> GetRegistsAsync()
        {
            var list = await _db2AdminDbContext.Regists.ToListAsync();
            return list;
        }
    }
}