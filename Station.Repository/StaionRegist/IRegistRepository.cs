using System.Collections.Generic;
using System.Threading.Tasks;
using Station.Entity.DB2Admin;

namespace Station.Repository.StaionRegist
{
    public interface IRegistRepository
    {
        Task<IList<Regist>> GetRegistsAsync();
    }
}