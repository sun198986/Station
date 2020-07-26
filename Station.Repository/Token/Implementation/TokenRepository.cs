using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using ServiceReference;

namespace Station.Repository.Token.Implementation
{
    [ServiceDescriptor(typeof(ITokenRepository), ServiceLifetime.Transient)]
    public class TokenRepository:ITokenRepository
    {
        private readonly TokenClient _tokenClient;
        public TokenRepository()
        {
            _tokenClient = WcfAdapter.WcfAdapterUtil.GetWcfClient<TokenClient>();
        }

        public async Task<string> GetToken(string userName, DateTime startDateTime, DateTime endDateTime)
        {
            return await _tokenClient.InsertToTokenAsync(userName, startDateTime, endDateTime, "", "", "");
        }
    }
}