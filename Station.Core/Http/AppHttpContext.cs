using System;
using Microsoft.AspNetCore.Http;

namespace Station.Core.Http
{
    public class AppHttpContext
    {
        private static IHttpContextAccessor m_httpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            m_httpContextAccessor = httpContextAccessor;

        }
        public static HttpContext Current
        {
            get
            {
                return m_httpContextAccessor.HttpContext;
            }
        }
        public static T GetSerivce<T>()
        {
            return (T)Current.RequestServices.GetService(typeof(T));
        }

      
        public static dynamic GetSerivce(Type type)
        {
            return (dynamic)Current.RequestServices.GetService(type);
        }
        public static bool IsPost
        {
            get
            {
                if (m_httpContextAccessor.HttpContext.Request.Method.ToLower().Equals("post"))
                {
                    return true;
                }
                return false;
            }
        }
        public static bool IsAjax
        {
            get
            {
                string sheader = Current.Request.Headers["X-Requested-With"];
                return (sheader != null && sheader == "XMLHttpRequest") ? true : false;

            }
        }

        /// <summary>
        /// 用户认证Key
        /// </summary>
        public const string TokenKeyName = "access_token";

    }
   
}