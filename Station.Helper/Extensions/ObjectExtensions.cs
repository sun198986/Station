using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace Station.Helper.Extensions
{
    public static class ObjectExtensions
    {
        public static ExpandoObject ShapeData<TSource>(this TSource source, string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var expandoObj = new ExpandoObject();
            if (string.IsNullOrWhiteSpace(fields))
            {
                var propertyInfos = typeof(TSource).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                foreach (var propertyInfo in propertyInfos)
                {
                    var propertyValue = propertyInfo.GetValue(source);
                    ((IDictionary<string, object>)expandoObj).Add(propertyInfo.Name, propertyValue);
                }
            }
            else
            {
                var fieldsAfterSplit = fields.Split(",");
                foreach (var field in fieldsAfterSplit)
                {
                    var propertyName = field.Trim();
                    var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (propertyInfo == null)
                    {
                        throw new Exception($"Property:{propertyName}没有找到:{typeof(TSource)}");
                    }
                    var propertyValue = propertyInfo.GetValue(source);
                    ((IDictionary<string, object>)expandoObj).Add(propertyInfo.Name, propertyValue);
                }
            }

            return expandoObj;
        }

        public static bool ToBool(this object obj)
        {
            return ConvertHelper.ObjToBool(obj);
        }

        public static DateTime? ToDateNull(this object obj)
        {
            return ConvertHelper.ObjToDateNull(obj);
        }

        public static decimal ToDecimal(this object obj)
        {
            return ConvertHelper.ObjToDecimal(obj);
        }

        public static decimal? ToDecimalNull(this object obj)
        {
            return ConvertHelper.ObjToDecimalNull(obj);
        }

        public static int ToInt(this object obj)
        {
            return ConvertHelper.ObjToInt(obj);
        }

        public static int? ToIntNull(this object obj)
        {
            return ConvertHelper.ObjToIntNull(obj);
        }


        public static string ToStr(this object obj)
        {
            return ConvertHelper.ObjToStr(obj);
        }
        /// <summary>
        /// 将对象转换为string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            if (obj != null)
            {
                return JsonHelper.ToJson(obj);
            }
            return "";
        }
    }
}