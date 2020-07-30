using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ServiceReference;
using Station.Core.Http;
using Station.Core.Login;

namespace Station.Core
{
    public class ApplicationContext:IApplicationContext
    {
        public HttpRequestLogUserModel CurrentUserLogInfo { get; set; }

        public string Token { get; set; }

        public void SetCurrentUserLogInfo(string myToken)
        {
            this.Token = myToken;
            string userLoginStr = SessionHelper.GetSession(this.Token);
            this.CurrentUserLogInfo = JsonConvert.DeserializeObject<HttpRequestLogUserModel>(userLoginStr);
        }

        public HttpRequestLogUserModel GetCurrentUserLogInfo()
        {
            string userLoginStr = SessionHelper.GetSession(this.Token);
            var userLogInfo = JsonConvert.DeserializeObject<HttpRequestLogUserModel>(userLoginStr);
            return userLogInfo;
        }

        public async Task<HttpRequestLogModel> GetContextModel()
        {
            var context = AppHttpContext.Current;
            HttpRequestLogModel contextModel = new HttpRequestLogModel
            {
                ActionUrl = AppHttpContext.Current.Request.Path,
                //UserId = UserId,
                // User = user,
                Headers = context.Request.Headers,
                Host = context.Request.Host,
                Query = context.Request.Query,
                Form = context.Request.HasFormContentType ? context.Request.Form : null,
                Body = await ReadRequestBody(context.Request, Encoding.UTF8),
                TraceIdentifier = AppHttpContext.Current.TraceIdentifier,
                Protocol = AppHttpContext.Current.Request.Protocol,
                IpAddress = AppHttpContext.Current.Request.Headers["X-Forwarded-For"].FirstOrDefault()?? AppHttpContext.Current.Connection.RemoteIpAddress.ToString(),
                Scheme = context.Request.Scheme,
                StartDate = DateTime.Now,
                ContentType = context.Request.ContentType,
                Method = context.Request.Method,
                Result = new ResponseResultModel()
            };
            return contextModel;
        }

        /// <summary>
        /// 读取请求正文
        /// </summary>
        /// <param name="request"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private async Task<string> ReadRequestBody(HttpRequest request, Encoding encoding)
        {
            var body = "";
            request.EnableBuffering();
            if (request.ContentLength == null ||
                !(request.ContentLength > 0) ||
                !request.Body.CanSeek)
            {
                return body;
            }
            request.Body.Seek(0, SeekOrigin.Begin);

            using (var reader = new StreamReader(request.Body, encoding, true, 1024, true))
            {
                body = await reader.ReadToEndAsync();
            }
            request.Body.Position = 0;
            return body;
        }
    }
}