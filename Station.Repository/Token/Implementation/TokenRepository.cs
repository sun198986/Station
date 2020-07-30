using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scrutor;
using ServiceReference;
using Station.Core;
using Station.Core.AppSettings;
using Station.Core.UserRoleWcf;
using Station.WcfAdapter;

namespace Station.Repository.Token.Implementation
{
    [ServiceDescriptor(typeof(ITokenRepository), ServiceLifetime.Transient)]
    public class TokenRepository:ITokenRepository
    {
        private readonly IApplicationContext _applicationContext;
        private readonly IOptions<Settings> _settings;
        private readonly TokenClient _tokenClient;
        public TokenRepository(IUserRoleControl wcfAdapter,IApplicationContext applicationContext, IOptions<Settings> settings)
        {
            _applicationContext = applicationContext;
            _settings = settings;
            _tokenClient = wcfAdapter.GetTokenClient();
        }

        public async Task<string> GetToken(string userName, DateTime startDateTime, DateTime endDateTime)
        {
            var requestLogInfo = await _applicationContext.GetContextModel();
            //machine???
            return await _tokenClient.InsertToTokenAsync(userName, startDateTime, endDateTime, requestLogInfo.IpAddress, "", _settings.Value.ServerAddress);
        }
    }
}