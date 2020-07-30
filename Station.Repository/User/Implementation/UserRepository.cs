using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using ServiceReference;
using Station.Core.UserRoleWcf;
using Station.WcfAdapter;

namespace Station.Repository.User.Implementation
{
    [ServiceDescriptor(typeof(IUserRepository), ServiceLifetime.Transient)]
    public class UserRepository:IUserRepository
    {
        private readonly UserClient _userClient;
        public UserRepository(IUserRoleControl wcfAdapter)
        {
            _userClient = wcfAdapter.GetUserClient();
        }

        public async Task<UserInfo> Login(string userName, string pwd)
        {
            return await _userClient.LoginAsync(pwd, userName);
        }
    }
}