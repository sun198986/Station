using ServiceReference;

namespace Station.WcfAdapter
{
    public static class WcfAdapterUtil
    {
        public static T GetWcfClient<T>() where T : class
        {
            
            if (typeof(T) == typeof(UserClient))//用户信息
            {
                return new UserClient(UserClient.EndpointConfiguration.WSHttpBinding_IUser, @"http://10.236.198.102:8888/ServiceControler/User") as T;
            }

            if (typeof(T) == typeof(CompanyClient))//公司信息
            {
                return new CompanyClient(CompanyClient.EndpointConfiguration.WSHttpBinding_ICompany, @"http://10.236.198.102:8888/ServiceControler/Company") as T;
            }

            if (typeof(T) == typeof(TokenClient))//授权信息
            {
                return new TokenClient(TokenClient.EndpointConfiguration.WSHttpBinding_IToken, @"http://10.236.198.102:8888/ServiceControler/Token") as T;
            }

            if (typeof(T) == typeof(UserRoleClient))//用户权限信息
            {
                return new UserRoleClient(UserRoleClient.EndpointConfiguration.WSHttpBinding_IUserRole, @"http://10.236.198.102:8888/ServiceControler/UserRole") as T;
            }

            if (typeof(T) == typeof(RoleClient))//权限信息
            {
                return new RoleClient(RoleClient.EndpointConfiguration.WSHttpBinding_IRole, @"http://10.236.198.102:8888/ServiceControler/Role") as T;
            }

            return default(T);
        }
    }
}