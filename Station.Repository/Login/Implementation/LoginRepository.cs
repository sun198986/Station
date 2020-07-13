using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using ServiceReference;
using Station.Repository.StaionRegist;

namespace Station.Repository.Login.Implementation
{
    [ServiceDescriptor(typeof(ILoginRepository), ServiceLifetime.Transient)]
    public class LoginRepository:ILoginRepository
    {
        private readonly UserClient _userClient;
        private readonly TokenClient _tokenClient;
        public LoginRepository()
        {
            _userClient = WcfAdapter.WcfAdapterUtil.GetWcfClient<UserClient>();
            _tokenClient = WcfAdapter.WcfAdapterUtil.GetWcfClient<TokenClient>();
        }
        public async Task<UserInfo> Login(string userName,string pwd)
        {
           return await _userClient.LoginAsync(pwd, userName);
        }

        public async Task<string> GetToken(string userName, DateTime startDateTime, DateTime endDateTime)
        {
            return await _tokenClient.InsertToTokenAsync(userName, startDateTime, endDateTime,"","","");
        }
    }
}