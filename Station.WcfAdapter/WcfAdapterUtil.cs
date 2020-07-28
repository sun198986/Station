using ServiceReference;

namespace Station.WcfAdapter
{
    public static class WcfAdapterUtil
    {
        public static T GetWcfClient<T>(string wcfUrl) where T : class
        {
            
            if (typeof(T) == typeof(UserClient))//用户信息
            {
                return new UserClient(UserClient.EndpointConfiguration.WSHttpBinding_IUser, $@"{wcfUrl}/ServiceControler/User") as T;
            }

            if (typeof(T) == typeof(CompanyClient))//公司信息
            {
                return new CompanyClient(CompanyClient.EndpointConfiguration.WSHttpBinding_ICompany, $@"{wcfUrl}/ServiceControler/Company") as T;
            }

            if (typeof(T) == typeof(TokenClient))//授权信息
            {
                return new TokenClient(TokenClient.EndpointConfiguration.WSHttpBinding_IToken, $@"{wcfUrl}/ServiceControler/Token") as T;
            }

            if (typeof(T) == typeof(UserRoleClient))//用户权限信息
            {
                return new UserRoleClient(UserRoleClient.EndpointConfiguration.WSHttpBinding_IUserRole, $@"{wcfUrl}/ServiceControler/UserRole") as T;
            }

            if (typeof(T) == typeof(RoleClient))//权限信息
            {
                return new RoleClient(RoleClient.EndpointConfiguration.WSHttpBinding_IRole, $@"{wcfUrl}/ServiceControler/Role") as T;
            }

            return default(T);
        }
    }
}