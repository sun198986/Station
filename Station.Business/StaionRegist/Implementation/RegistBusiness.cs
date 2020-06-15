using System.Collections.Generic;
using System.Threading.Tasks;
using Station.Entity.DB2Admin;
using Station.Repository.StaionRegist;

namespace Station.Business.StaionRegist.Implementation
{
    public class RegistBusiness:IRegistBusiness
    {
        private readonly IRegistRepository _registRepository;

        public RegistBusiness(IRegistRepository registRepository)
        {
            _registRepository = registRepository;
        }

        public async Task<IList<Regist>> GetRegistsAsync()
        {
            var list = await _registRepository.GetRegistsAsync();
            return list;
        }
    }
}