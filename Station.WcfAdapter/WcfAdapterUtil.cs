using ServiceReference;

namespace Station.WcfAdapter
{
    public static class WcfAdapterUtil
    {
        public static T GetWcfClient<T>() where T : class
        {
            if (typeof(T) == typeof(UserClient))
            {
                return new UserClient(UserClient.EndpointConfiguration.WSHttpBinding_IUser, @"http://10.236.198.102:8888/ServiceControler/User") as T;
            }else if (typeof(T) == typeof(CompanyClient))
            {
                return new UserClient(UserClient.EndpointConfiguration.WSHttpBinding_IUser, @"http://10.236.198.102:8888/ServiceControler/User") as T;
            }

            return default(T);
        }
    }
}