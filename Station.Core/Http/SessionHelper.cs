using System;
using Microsoft.AspNetCore.Http;

namespace Station.Core.Http
{
    public class SessionHelper
    {
        private static HttpResponse CurrentResponse
        {
            get {
                return AppHttpContext.Current.Response;
            }
        }
        private static HttpRequest CurrentRequest
        {
            get
            {
                return AppHttpContext.Current.Request;
            }
        }
        public static void Add(string key, string value)
        {
            AppHttpContext.Current.Session.SetString(key,value);
        }
        public static void Delete(string key)
        {
            AppHttpContext.Current.Session.Remove(key);
        }
        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        public static string GetSession(string key)
        {
            return AppHttpContext.Current.Session.GetString(key);
          
        }
    }
}
