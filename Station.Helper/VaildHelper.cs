using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Station.Helper
{
    /// <summary>
    /// 自定义数据数据类型
    /// </summary>
    public enum CustomDataType
    {
        /// <summary>
        /// 整型
        /// </summary>
        Int,
        /// <summary>
        /// 小数
        /// </summary>
        Decimal,
        /// <summary>
        /// 邮件格式
        /// </summary>
        Email,
        /// <summary>
        /// 联系电话
        /// </summary>
        Phone,
        /// <summary>
        /// 中文
        /// </summary>
        CHZN,
        /// <summary>
        /// ip地址
        /// </summary>
        Ip,
        /// <summary>
        /// 链接地址
        /// </summary>
        Url,
        /// <summary>
        /// 列表数据项 split: ,/;/，/；/|
        /// </summary>
        List,
        /// <summary>
        /// 其它数据类型
        /// </summary>
        Other

    }
    /// <summary>
    /// 系统工具类
    /// </summary>
    public class VaildHelper
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        public const string VAILD_ISEXIST = "Vaild_IsExist";

        #region ErrorCode

        #endregion 

        /// <summary>
        /// 验证值是否有效
        /// </summary>
        /// <param name="IsDBNULL">是否允许为空,true表示允许</param>
        /// <param name="vaildKey">数据类型</param>
        /// <param name="IsNegative">是否允许非负数</param>
        /// <param name="value">当前的值</param>
        /// <param name="minLength">最小长度0表示不限</param>
        /// <param name="MaxLength">字符串最长多少位0表示不限</param>
        /// <param name="Min">最小值</param>
        /// <param name="Max">最大值</param>
        /// <param name="strError">错误提示信息</param>
        /// <param name="regex">自定义正则表达式规则</param>
        /// <param name="sqlSafeCheck">检测是否有Sql危险字符</param>
        /// <returns></returns>
        public static bool CheckInput(string localName,bool IsDBNULL,string vaildKey,bool IsNegative,string value,
            int MinLength, int MaxLength, decimal Min, decimal Max, string regex, ref string strError, string columnType="")
        {
            strError = "";
            value = value.Trim();
            if (IsDBNULL)
            {
                if (value == "")
                    return true;
            }
            else
            {
                if (value == "")
                {
                    strError = localName+"不允许为空!";
                  
                    return false;
                }
            }
            if (
                columnType.ToLower().Equals("string")
                && !vaildKey.Equals("IsDate")
                && !vaildKey.Equals("number")
                && !vaildKey.Equals("IsInt")
                && !vaildKey.Equals("IsDecimal")
                && !vaildKey.Equals("IsList")
                )
            {
                if (MinLength > 0)
                {
                    if (value.Length < MinLength)
                    {
                        strError = localName + "字数太少，小于系统限定的" + MinLength.ToString() + "个字";
                        return false;
                    }
                }
                if (MaxLength > 0)
                {
                    if (value.Length > MaxLength)
                    {
                        strError = localName + "字数太长，大于系统限定的" + MaxLength.ToString() + "个字";
                        return false;
                    }
                }
            }
            //if (sqlSafeCheck)
            //{
            //    if (IsSafeSqlString(value))
            //    {
            //        strError = "包含危险的字符！";
            //        return false;
            //    }
            //}
            switch (vaildKey)
            {
                case "IsInt":
                case "number":
                    #region Int
                    if (IsInt(value, IsNegative))
                    {
                        if (Min!=0&&int.Parse(value) < Min)
                        {
                            strError = localName + "输入的数字太小，小于系统设定的最小值" +Min.ToString();
                            return false;
                        }
                        if (Max !=-1&&Max!=0 && int.Parse(value)> Max)
                        {
                            strError = localName + "输入的数字太大，已超过了系统设定的最大值" + Max.ToString();
                            return false;
                        }
                    }
                    else
                    {
                        strError = localName + "不是一个有效的整数！";
                        return false;
                    }
                    #endregion
                    break;
                case "IsDecimal": 
                    #region IsDecimal
                    if (IsDecimal(value, IsNegative))
                    {
                        if (Min != 0 && decimal.Parse(value) < Min)
                        {
                            strError = localName + "输入的数字太小，小于系统设定的最小值" + Min.ToString()+"！";
                            return false;
                        }
                        if (Max != 0 && decimal.Parse(value) > Max)
                        {
                            strError = localName + "您输入的数字太大，已超过了系统设定的最大值" + Max.ToString() + "！";
                            return false;
                        }
                    }
                    else
                    {
                        strError = localName + "不是一个有效的数字！";
                        return false;
                    }
                    #endregion
                    break;
                case "IsList":
                    #region List
                    string[] sArray = value.Split(new char[] { ',',';','，','；','|'});
                    if (sArray.Length < Min)
                    {
                        strError = localName + "选择的项低于系统设定" + Min.ToString() + "项！";
                        return false;
                    }
                    if (sArray.Length > Max)
                    {
                        strError = localName + "选择的项高于系统设定" + Min.ToString() + "项！";
                        return false;
                    }
                    #endregion
                    break;
                case "IsEmail":
                    if (!IsEmail(value))
                    {
                        strError = localName + "不是一个有效的邮件格式！";
                        return false;
                    }
                    break;
                case  "IsIP":
                    if (!IsIP(value))
                    {
                        strError = localName + "不是一个有效的IP！";
                        return false;
                    }
                    break;
                case "IsCHZN"://包含中文
                    if (IsCHZN(value))
                    {
                        strError = localName + "没有包含中文！";
                        return false;
                    }
                    break;
                case "IsPhone":
                    if (!IsPhone(value))
                    {
                        strError = localName + "不是一个有效的手机格式！";
                        return false;
                    }
                    break;
                case "IsUrl":
                    if (!IsURL(value))
                    {
                        strError = localName + "不是一个有效的链接地址！";
                        return false;
                    }
                    break;
                case "stringEn":
                    if (!IsStringEn(value))
                    {
                        strError = localName + "只能输入字母数字和下划线！";
                        return false;
                    }
                    break;
                default:
                    if (regex!=null&&regex != "")
                    {
                        if (!Regex.IsMatch(value, regex))
                        {
                            strError = localName + "格式不正确";
                            return false;
                        }
                    }
                    break;
            }
            return true;
        }

        ///// <summary>
        ///// 验证数据格式是否有效
        ///// </summary>
        ///// <param name="entityObj"></param>
        ///// <param name="strError"></param>
        ///// <returns></returns>
        //public static bool CheckVaild(object entityObj,ref ModelBindingContext bindingContext)
        //{
        //    List<DbColumnAttribute> lstAttr = ReflectionHelper.getPropertyAttribute(entityObj);
        //    var entityType = entityObj.GetType();
        //    if (lstAttr != null)
        //    {

        //        foreach (DbColumnAttribute item in lstAttr)
        //        {
        //            if (item.IsSystem || item.IsHide || !item.IsEnabled || item.IsPrimaryKey)
        //            {
        //                continue;
        //            }
        //            string strError = "";
        //            if (!CheckInput(item.LocalName,(item.IsPrimaryKey?true:item.AllowDBNull), "", true,
        //                   (item.ObjValue != null ? item.ObjValue.ToString() : ""), 0, item.MaxLength, item.Min, item.Max, item.RegValidate, ref strError))
        //            {

        //                bindingContext.ModelState.AddModelError(item.Name,strError);

        //            }
        //        }
        //    }
        //    return true;
        //}

        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }
  
        /// <summary>
        /// 验证是否是整数
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="IsNegative">是否允许为负数,true表示允许</param>
        /// <returns></returns>
        public static bool IsInt(string str, bool IsNegative)
        {
            if (IsNegative)
                return Regex.IsMatch(str, @"^-?\d+$");
            return Regex.IsMatch(str, @"^\d+$");
        }

        /// <summary>
        /// 验证是否是一个有效的数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="IsNegative">是否允许为负数</param>
        /// <returns></returns>
        public static bool IsDecimal(string str, bool IsNegative)
        {
            if (IsNegative)
                return Regex.IsMatch(str, @"^(-?\d+)(\.\d+)?$");
            return Regex.IsMatch(str, @"^(\d+)(\.\d+)?$");
        }

        /// <summary>
        /// 验证是否是一个有效IP
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIP(string str)
        {
            return Regex.IsMatch(str, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 验证是否包含中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsCHZN(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }

        public static bool IsStringEn(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z0-9_@.]+$");
        }

        

        /// <summary>
        /// 手机号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPhone(string str)
        {
            return Regex.IsMatch(str, @"^[1][0-9]\d{9}$");
        }

        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        public static string GetEmailHostName(string strEmail)
        {
            if (strEmail.IndexOf("@") < 0)
            {
                return "";
            }
            return strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
        }

        /// <summary>
        /// 判断是否为base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64String(string str)
        {
            //A-Z, a-z, 0-9, +, /, =
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 检测是否有危险的可能用于链接的字符串
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|游客|^Guest");
        }

        /// <summary>
        /// 清理字符串
        /// </summary>
        public static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn.Trim(), @"[^\w\.@-]", "");
        }

        ///// <summary>
        ///// 将字符串转换为Color
        ///// </summary>
        ///// <param name="color"></param>
        ///// <returns></returns>
        //public static Color ToColor(string color)
        //{
        //    int red, green, blue = 0;
        //    char[] rgb;
        //    color = color.TrimStart('#');
        //    color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
        //    switch (color.Length)
        //    {
        //        case 3:
        //            rgb = color.ToCharArray();
        //            red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
        //            green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
        //            blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
        //            return Color.FromArgb(red, green, blue);
        //        case 6:
        //            rgb = color.ToCharArray();
        //            red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
        //            green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
        //            blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
        //            return Color.FromArgb(red, green, blue);
        //        default:
        //            return Color.FromName(color);

        //    }
        //}

        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            if (strNumber == null)
                return false;

            if (strNumber.Length < 1)
                return false;

            foreach (string id in strNumber)
            {
                if (!VaildHelper.IsInt(id,false))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (!StrIsNullOrEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                    return new string[] { strContent };

                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
                return new string[0] { };
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, int count)
        {
            string[] result = new string[count];
            string[] splited = SplitString(strContent, strSplit);

            for (int i = 0; i < count; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// 字段串是否为Null或为""(空)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StrIsNullOrEmpty(string str)
        {
            if (str == null || str.Trim() == string.Empty)
                return true;

            return false;
        }

        /// <summary>
        /// 检测用户名格式是否正确
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidUser(string str)
        {
            return !Regex.IsMatch(str, @"[a-zA-Z][a-zA-Z0-9_-]{5,15}");
        }

        /// <summary>
        /// 检测用户名格式是否正确
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidPwd(string str)
        {
            return !Regex.IsMatch(str, @"(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,16}");
        }
        public static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.
            GetGenericTypeDefinition().Equals
            (typeof(Nullable<>)));
        }  

    }
}
