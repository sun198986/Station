using System.Collections.Generic;
using System.Threading.Tasks;
using Station.Entity.DB2Admin;

namespace Station.Business.StaionRegist
{
    public interface IRegistBusiness
    {
        Task<IList<Regist>> GetRegistsAsync();
    }
}