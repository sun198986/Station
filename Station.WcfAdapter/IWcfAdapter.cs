using ServiceReference;

namespace Station.WcfAdapter
{
    public interface IWcfAdapter
    {
        //tokenClient
        TokenClient GetTokenClient();

        //userClient
        UserClient GetUserClient();

        //companyClient
        CompanyClient GetCompanyClient();

        //UserRoleClient
        UserRoleClient GetUserRoleClient();

        //RoleClient
        RoleClient GetRoleClient();
    }
}