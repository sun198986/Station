using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using ServiceReference;
using Station.WcfAdapter;

namespace Station.Repository.Token.Implementation
{
    [ServiceDescriptor(typeof(ITokenRepository), ServiceLifetime.Transient)]
    public class TokenRepository:ITokenRepository
    {
        private readonly TokenClient _tokenClient;
        public TokenRepository(IWcfAdapter wcfAdapter)
        {
            _tokenClient = wcfAdapter.GetTokenClient();
        }

        public async Task<string> GetToken(string userName, DateTime startDateTime, DateTime endDateTime)
        {
            return await _tokenClient.InsertToTokenAsync(userName, startDateTime, endDateTime, "", "", "");
        }
    }
}