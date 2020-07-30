using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using Station.Helper.Extensions;

namespace Station.Helper
{
    public class ConvertHelper
    {
        public static bool ObjToBool(object obj)
        {
            bool flag;
            if (obj == null)
            {
                return false;
            }
            if (obj.Equals(DBNull.Value))
            {
                return false;
            }
            if (obj.ToString().Equals("1"))
            {
                return true;
            }
            return (bool.TryParse(obj.ToString(), out flag) && flag);
        }
        
        public static DateTime? ObjToDateNull(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            try
            {
                return new DateTime?(Convert.ToDateTime(obj));
            }
            catch
            {
                return null;
            }
        }
        
        public static decimal ObjToDecimal(object obj)
        {
            if (obj == null)
            {
                return 0M;
            }
            if (obj.Equals(DBNull.Value))
            {
                return 0M;
            }
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return 0M;
            }
        }
        
        public static decimal? ObjToDecimalNull(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if (obj.Equals(DBNull.Value))
            {
                return null;
            }
            return new decimal?(ObjToDecimal(obj));
        }
        
        public static int ObjToInt(object obj)
        {
            if (obj != null)
            {
                int num;
                if (obj.Equals(DBNull.Value))
                {
                    return 0;
                }
                if (int.TryParse(obj.ToString(), out num))
                {
                    return num;
                }
            }
            return 0;
        }
        
        public static int? ObjToIntNull(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if (obj.Equals(DBNull.Value))
            {
                return null;
            }
            return new int?(ObjToInt(obj));
        }
        
        public static string ObjToStr(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            if (obj.Equals(DBNull.Value))
            {
                return "";
            }
            return Convert.ToString(obj);
        }

        #region .net类型互相转换

        /// <summary>
        /// 把输入的string类型的数据转换成entity字段值
        /// </summary>
        /// <param name="input">输入的对象</param>
        /// <returns>数据库格式的数据</returns>
        public static void ToTypeValue(PropertyInfo property,object model, string input)
        {
            var type = property.PropertyType.GenericTypeArguments.Length >= 1 ? property.PropertyType.GenericTypeArguments[0] : property.PropertyType;
            if (type == typeof(int))
            {
                property.SetValue(model, ObjToInt(input));
            }
            else if (type == typeof(short))
            {
                
                property.SetValue(model, Convert.ToInt16(input)); 
            }
            else if (type == typeof(long))
            {
                property.SetValue(model, Convert.ToInt64(input));
            }
            else if (type == typeof(DateTime))
            {
                property.SetValue(model, Convert.ToDateTime(input));
            }
            else if (type == typeof(double))
            {
                property.SetValue(model, Convert.ToDouble(input));
            }
            else if (type == typeof(Decimal))
            {
                property.SetValue(model, Convert.ToDecimal(input));
            }
            else if (type == typeof(Byte))
            {
                property.SetValue(model, Convert.ToByte(input));
            }
           
            else if (type == typeof(bool))
            {
                property.SetValue(model, Convert.ToBoolean(input));
            }

            property.SetValue(model,input+"  ");
        }
        /// <summary>
        /// 把String转换为Double
        /// </summary>
        /// <param name="input">string数据</param>
        /// <returns>double数据，若无效返回0</returns>
        public static Double ToDouble(string input)
        {
            if (input.Trim() != "")
            {
                try
                {
                    return Convert.ToDouble(input);
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        /// <summary>
        /// 把String转换为Decimal
        /// </summary>
        /// <param name="input">string数据</param>
        /// <returns>decimal数据，若无效返回0</returns>
        public static Decimal ToDecimal(string input)
        {
            if (input.Trim() != "")
            {
                try
                {
                    return Convert.ToDecimal(input);
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        public static Decimal ToDecimal(object input)
        {

            try
            {
                return Convert.ToDecimal(input);
            }
            catch
            {
                return 0;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object input)
        {
            if (input == null || input == System.DBNull.Value)
            {
                return DateTime.MinValue;
            }
            try
            {
                return Convert.ToDateTime(input.ToString());
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 把String转换为Decimal
        /// </summary>
        /// <param name="input">string数据</param>
        /// <returns>decimal数据，若无效返回0</returns>
        public static Int32 ToInt32(string input)
        {
            if (input.Trim() != "")
            {
                try
                {
                    return Convert.ToInt32(input);
                }
                catch
                {
                    return ToInt32(bool.Parse(input));
                }
            }
            return 0;
        }

        public static Int32 ToInt32(bool input)
        {
            if (input)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToString(object input)
        {
            if (input == null || input == DBNull.Value)
            {
                return "";
            }
            else
            {
                return input.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ToBoolean(object input)
        {
            if (input == null || input == DBNull.Value)
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(input);
            }
        }

        /// <summary>
        /// 把DateTime转换为ShortDateString
        /// </summary>
        /// <param name="input">DateTime数据</param>
        /// <returns>ShortDateString</returns>
        public static string ToShortDateString(DateTime input)
        {
            if (input.Date == new DateTime(1900, 1, 1) || input.Date == new DateTime(0001, 1, 1))
            {
                return "";
            }
            else
            {
                return input.ToShortDateString();
            }
        }

        /// <summary>
        /// 把DateTime转换为DateString
        /// </summary>
        /// <param name="input">DateTime数据</param>
        /// <returns>DateString</returns>
        public static string ToDateString(DateTime input)
        {
            if (input.Date == new DateTime(1900, 1, 1) || input.Date == new DateTime(0001, 1, 1))
            {
                return "";
            }
            else
            {
                return input.ToString();
            }
        }

        /// <summary>
        /// 获取枚举描述或者名称
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IsEnumName">是否读取枚举名称</param>
        /// <returns></returns>
        public static Dictionary<int, string> ToEnumList(Type obj, bool IsEnumName)
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            FieldInfo[] fields = obj.GetFields();
            foreach (FieldInfo item1 in fields)
            {
                string enumName =item1.Name;
                if (enumName.ToLower().IndexOf("value__") >= 0)
                {
                    continue;
                }

                if (!IsEnumName)
                {
                    var t = item1.GetCustomAttribute<DescriptionAttribute>();
                    if (t != null)
                    {
                        enumName = t.Description;
                    }
                }
                list.Add(Enum.Parse(obj, item1.Name).GetHashCode(),enumName);
             
            }
           
            return list;
        }
       
       
        #endregion

        #region ToSqlString
        /// <summary>
        /// 把输入的日期型的字符串转化为数据库格式的数据
        /// </summary>
        /// <param name="input">输入的日期型的字符串</param>
        /// <returns>数据库格式的数据</returns>
        public static string ToSqlString(DateTime input)
        {
            if (input.Date == new DateTime(1900, 1, 1) || input.Date == new DateTime(0001, 1, 1))
            {
                return "null";
            }
            else
            {
                return "'" + input.ToString().Trim().Replace("'", "''") + "'";
            }
        }

        /// <summary>
        /// 把输入的字符串转化为数据库格式的数据
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>数据库格式的数据</returns>
        public static string ToSqlString(string input)
        {
            return "'" + input.ToString().Trim().Replace("'", "''") + "'";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSqlString(int input)
        {
            return input.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSqlString(short input)
        {
            return input.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSqlString(long input)
        {
            return input.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSqlString(decimal input)
        {
            return input.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSqlString(double input)
        {
            return input.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSqlString(float input)
        {
            return input.ToString();
        }

        /// <summary>
        /// 把输入的对象转化为数据库格式的数据
        /// </summary>
        /// <param name="input">输入的对象</param>
        /// <returns>数据库格式的数据</returns>
        public static string ToSqlString(object input)
        {
            if (input == null)
            {
                return "null";
            }
            if (input == DBNull.Value)
            {
                return "null";
            }
            if (input is DateTime)
            {
                return ToSqlString((DateTime)input);
            }
            if (input is int)
            {
                return ToSqlString((int)input);
            }
            if (input is short)
            {
                return ToSqlString((short)input);
            }
            if (input is long)
            {
                return ToSqlString((long)input);
            }
            if (input is decimal)
            {
                return ToSqlString((decimal)input);
            }
            if (input is double)
            {
                return ToSqlString((double)input);
            }
            if (input is float)
            {
                return ToSqlString((float)input);
            }
            return "'" + input.ToString().Trim().Replace("'", "''") + "'";
        }
        #endregion


        #region sql to Entity
        /// <summary>
        /// 实体转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> ToEntityList<T>(IDataReader reader) where T : class,new()
        {
            List<T> lstT = new List<T>();
            DataTable schemaTable = reader.GetSchemaTable();
            PropertyInfo[] pArray = typeof(T).GetProperties();
            while (reader.Read())
            {
                T entity = new T();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string fieldName = schemaTable.Rows[i][0].ToString();
                    foreach (PropertyInfo p in pArray)
                    {
                        if (fieldName.ToLower() == p.Name.ToLower())
                        {
                            object value = reader.GetValue(i);
                            if (value != DBNull.Value)
                            {

                                SetValue(entity, p, value);
                            }
                            break;
                        }
                    }
                }
                lstT.Add(entity);
            }

            return lstT;
        }
        /// <summary>
        /// 实体转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> ToHashTableList<T>(IDataReader reader) where T : class, new()
        {
           
            List<Dictionary<string, object>> lstT = new List<Dictionary<string, object>>();
            DataTable schemaTable = reader.GetSchemaTable();
         
            while (reader.Read())
            {
                Dictionary<string, object> ht = new Dictionary<string, object>();
            
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string fieldName = schemaTable.Rows[i][0].ToString();
                    object value = reader.GetValue(i);
                    ht.Add(fieldName, value);
                }
                 
                lstT.Add(ht);
            }
            reader.Close();
            return lstT;
        }

        public static void SetValue<T>(T entity,PropertyInfo p, object value)
        {
            switch (p.PropertyType.Name.ToLower())
            {
                case "boolean":
                    p.SetValue(entity, value.ToBool(), null);
                    break;
                default:
                    p.SetValue(entity, value, null);
                    break;
            }
          
        }
        #endregion
    }
}
