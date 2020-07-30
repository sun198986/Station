using Microsoft.Extensions.Options;
using ServiceReference;
using Station.Core.AppSettings;
using Station.WcfAdapter;

namespace Station.Core.UserRoleWcf
{
    public class UserRoleControl:Core.UserRoleWcf.IUserRoleControl
    {
        private readonly IOptions<Settings> _settings;
        public UserRoleControl(IOptions<Settings> settings)
        {
            _settings = settings;
            
        }
        public TokenClient GetTokenClient()
        {
            return WcfAdapterUtil.GetWcfClient<TokenClient>(_settings.Value.WcfUrl);
        }

        public UserClient GetUserClient()
        {
            return WcfAdapterUtil.GetWcfClient<UserClient>(_settings.Value.WcfUrl);
        }

        public CompanyClient GetCompanyClient()
        {
            return WcfAdapterUtil.GetWcfClient<CompanyClient>(_settings.Value.WcfUrl);
        }

        public UserRoleClient GetUserRoleClient()
        {
            return WcfAdapterUtil.GetWcfClient<UserRoleClient>(_settings.Value.WcfUrl);
        }

        public RoleClient GetRoleClient()
        {
            return WcfAdapterUtil.GetWcfClient<RoleClient>(_settings.Value.WcfUrl);
        }
    }
}