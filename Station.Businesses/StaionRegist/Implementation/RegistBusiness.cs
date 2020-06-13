using System.Collections.Generic;
using System.Threading.Tasks;
using Station.Entities.DB2Admin;
using Station.Repositories.StaionRegist;

namespace Station.Businesses.StaionRegist.Implementation
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