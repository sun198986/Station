using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using ServiceReference;

namespace Station.Repository.User.Implementation
{
    [ServiceDescriptor(typeof(IUserRepository), ServiceLifetime.Transient)]
    public class UserRepository:IUserRepository
    {
        private readonly UserClient _userClient;
        public UserRepository()
        {
            _userClient = WcfAdapter.WcfAdapterUtil.GetWcfClient<UserClient>();
        }


        public async Task<UserInfo> Login(string userName, string pwd)
        {
            return await _userClient.LoginAsync(pwd, userName);
        }
    }
}