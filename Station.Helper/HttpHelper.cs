using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Station.Helper
{
    public class SendHttpHelper
    {
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }

        public static bool XmlPost(string xml, string url, string certPath, string certPwd, int timeout, ref string strResult)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;
            bool flag = false;
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = timeout * 1000;
                //设置POST的数据类型和长度
                request.ContentType = "text/xml";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                request.ContentLength = data.Length;

                //是否使用证书
                if (!string.IsNullOrEmpty(certPath))
                {
                    X509Certificate2 cert = new X509Certificate2(AppDomain.CurrentDomain.BaseDirectory + certPath, certPwd);
                    request.ClientCertificates.Add(cert);

                }

                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                strResult = sr.ReadToEnd().Trim();
                sr.Close();
                flag = true;
            }
            catch (Exception)
            {
                System.Threading.Thread.ResetAbort();
                flag = false;
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return flag;
        }

        /// <summary>
        /// 发送普通请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="parameters">参数</param>
        /// <param name="charset">字符集</param>
        /// <param name="strResult">返回结果</param>
        /// <param name="isPost">是否采用post提交</param>
        /// <returns></returns>
        public static bool SendReq(string url, SortedDictionary<string, string> parameters, string charset, 
            ref string strResult, bool isPost = true)
        {

            //HTTPSQ请求
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            StringBuilder buffer = new StringBuilder();
            
            //如果需要POST数据   
            if ( parameters != null && parameters.Count > 0)
            {
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }

            }
            //else
            //{
            //    strResult = "参数为空无法发送请求";
            //    return false;
            //}
            try
            {
                if (!isPost)
                {
                    if (url.IndexOf("?") != -1)
                    {
                        url += "&";
                    }
                    else
                    {
                        url += "?";
                    }
                    url += buffer;
                }

                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.Timeout = 12000;
                myReq.ServicePoint.Expect100Continue = false;
                //myReq.ProtocolVersion = HttpVersion.Version10;
                myReq.Method = isPost ? "POST" : "GET";
                myReq.ContentType = "application/x-www-form-urlencoded;charset=" + charset;
                if (isPost)
                {
                    //myReq.Headers.Add("appAjax", "1");
                    byte[] data = Encoding.GetEncoding(charset).GetBytes(buffer.ToString());
                    using (Stream stream = myReq.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.GetEncoding(charset));
                strResult = sr.ReadToEnd();   //从头读到尾，放到字符串html
                return true;
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }
            return false;
        }

        //public static bool SendWebClient(string url,string data,string userToken,ref string strResult, int timeout=10000, bool isPost=true)
        //{
        //    try
        //    {
        //        WebClientHelper clientHelper = new WebClientHelper();
        //        clientHelper.Headers.Add("CONTENT-TYPE", "application/json");
        //        clientHelper.Headers.Add(AppHttpContext.TokenKeyName, userToken);
        //        clientHelper.Timeout = timeout;
        //        strResult = clientHelper.UploadString(url, isPost ? "POST" : "GET", data);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error(ex,ex);
        //    }
        //    return true;
           
        //}
    }


    /// <summary>
    /// WebClient 超时设置
    /// </summary>
    public class WebClientHelper : WebClient
    {
        private int _timeout = 10000;
        // 超时时间(毫秒)
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }
        public WebClientHelper()
        {
           
        }
        public WebClientHelper(int timeout)
        {
            this._timeout = timeout;
        }
      
        protected override WebRequest GetWebRequest(Uri address)
        {
            var result = base.GetWebRequest(address);
            result.Timeout = this._timeout;
            return result;
        }
    }
}

