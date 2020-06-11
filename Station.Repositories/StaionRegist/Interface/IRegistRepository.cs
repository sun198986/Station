using System.Collections.Generic;
using System.Threading.Tasks;
using Station.Entities.DB2Admin;

namespace Station.Repositories.StaionRegist.Interface
{
    public interface IRegistRepository
    {
        Task<IList<Regist>> GetRegistsAsync();
    }
}