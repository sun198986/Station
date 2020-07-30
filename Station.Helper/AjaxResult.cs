using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetCore
{
    /// <summary>
    /// 前台Ajax请求的统一返回结果类
    /// </summary>
    public class AjaxResult
    {
        public AjaxResult()
        {
            StatusCode = 200;
        }

        private bool iserror = false;

        /// <summary>
        /// 是否产生错误
        /// </summary>
        public bool IsError { get { return iserror; } }

        /// <summary>
        /// 错误信息，或者成功信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误信息，或者成功信息
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 成功可能时返回的数据
        /// </summary>
        public object Data { get; set; }

        #region Error
        public static AjaxResult Error()
        {
            return new AjaxResult()
            {
                iserror = true
            };
        }
        public static AjaxResult Error(string message)
        {
            return new AjaxResult()
            {
                iserror = true,
                StatusCode=-1,
                Message = message
            };
        }
        public static AjaxResult Error(string message,int statusCode)
        {
            return new AjaxResult()
            {
                iserror = true,
                StatusCode=statusCode,
                Message = message
            };
        }

        /// <summary>
        /// 获取验证的错误信息
        /// </summary>
        /// <param name="modelState"></param>
        public static string GetModelError(ModelStateDictionary modelState)
        {
            string strError = "";
            foreach (string key in modelState.Keys)
            {
                foreach (var p in modelState[key].Errors)
                {
                    strError += "\n " + key + ":" + p.ErrorMessage;
                }
            }
            return strError;
        }
        public static AjaxResult Error(ModelStateDictionary modelState)
        {
            string strError = GetModelError(modelState);
            return new AjaxResult()
            {
                iserror = true,
                Data = modelState,
                Message= strError
            };
        }
        #endregion

        #region Success
        public static AjaxResult Success()
        {
            return new AjaxResult()
            {
                iserror = false,
                StatusCode=0
            };
        }
        public static AjaxResult Success(string message)
        {
            return new AjaxResult()
            {
                iserror = false,
                StatusCode=0,
                Message = message
            };
        }
        public static AjaxResult Success(object data)
        {
            return new AjaxResult()
            {
                iserror = false,
                StatusCode=0,
                Data = data
            };
        }
        public static AjaxResult Success(object data, string message)
        {
            return new AjaxResult()
            {
                iserror = false,
                StatusCode=0,
                Data = data,
                Message = message
            };
        }
        #endregion

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}