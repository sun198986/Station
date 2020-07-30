using ServiceReference;

namespace Station.Core.UserRoleWcf
{
    public interface IUserRoleControl
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