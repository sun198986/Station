using System;
using Microsoft.AspNetCore.Http;

namespace Station.Core.Login
{
    /// <summary>
    /// Request 帮助信息类
    /// </summary>
    public class HttpRequestLogModel
    {
       public IHeaderDictionary Headers { get; set; }

        /// <summary>
        /// 请求唯一id
        /// </summary>
        public string TraceIdentifier { get; set; }

        /// <summary>
        /// 请求相对路径
        /// </summary>
        public string ActionUrl { get; set; }

        public HostString Host { get; set; }

        public IQueryCollection Query { get; set; }
        //
        // 摘要:
        //     Gets or sets the request body as a form.
        public  IFormCollection Form { get; set; }

        /// <summary>
        /// 获取请求 协议
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// 获取url的Scheme
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// 客户端ip
        /// </summary>
        public string IpAddress { get; set; }

        public string ContentType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 请求内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 请求结果
        /// </summary>
        public ResponseResultModel Result { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        public object User { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string ApplicationName
        {
            get;
            set;
        }

        public string ServerAddress { get; set; }


    }

    /// <summary>
    /// 请求结果
    /// </summary>
    public class ResponseResultModel
    {
        /// <summary>
        /// 是否存在错误
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set; }

        public string Content { get; set; }

        public string ContentType { get; set; }

        /// <summary>
        /// 请求耗时(单位毫秒)
        /// </summary>
        public int TaskTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 返回头部
        /// </summary>
        public IHeaderDictionary Headers { get; set; }
    }
}
