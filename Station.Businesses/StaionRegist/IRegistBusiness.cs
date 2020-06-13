using System.Collections.Generic;
using System.Threading.Tasks;
using Station.Entities.DB2Admin;

namespace Station.Businesses.StaionRegist
{
    public interface IRegistBusiness
    {
        Task<IList<Regist>> GetRegistsAsync();
    }
}