using ServiceReference;

namespace Station.Core.Login
{
    public class HttpRequestLogUserModel
    {
        public HttpRequestLogUserModel(UserInfo userInfo)
        {
            this.CurrentUserInfo = userInfo;
        }

        public UserInfo CurrentUserInfo { get; set; }

        public CompanyInfo CurrentCompanyInfo { get; set; }

        public UserRoleInfo CurrentUserRoleInfo { get; set; }
    }
}