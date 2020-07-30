using System.Threading.Tasks;
using ServiceReference;
using Station.Core.Login;

namespace Station.Core
{
    public interface IApplicationContext
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        HttpRequestLogUserModel CurrentUserLogInfo { get; set; }

        /// <summary>
        /// 授权字符串
        /// </summary>

        public string Token { get; set; }

        /// <summary>
        /// 设置当前登录信息
        /// </summary>
        void SetCurrentUserLogInfo(string myToken);

        /// <summary>
        /// 获取当前登录的信息
        /// </summary>
        /// <returns></returns>
        HttpRequestLogUserModel GetCurrentUserLogInfo();

        /// <summary>
        /// 获取当前请求信息
        /// </summary>
        /// <returns></returns>
        Task<HttpRequestLogModel> GetContextModel();
    }
}