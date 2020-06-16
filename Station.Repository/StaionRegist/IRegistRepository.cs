using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Station.Entity.DB2Admin;

namespace Station.Repository.StaionRegist
{
    public interface IRegistRepository
    {
        Task<IEnumerable<Regist>> GetRegistsAsync();

        Task<IEnumerable<Regist>> GetRegistsAsync(IEnumerable<string> companyIds);
    }
}