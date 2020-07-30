using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Station.Helper
{
    /// <summary>
    /// 有关反射的方法   
    /// 主要是从DLL里创建对象及获取嵌入的资源        
    /// </summary>
    public class ReflectionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public ReflectionHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="fullName">程序集 以及类路径</param>
        /// <returns></returns>
        public static object Create(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)|| fullName.IndexOf(";")==-1)
            {
                return null;
            }
            var arr = fullName.Split(';');
            return CreateInstance(arr[0],arr[1],null) ;
        }

        /// <summary>
        /// 从DLL里创建对象的实例
        /// </summary>
        /// <param name="assemblyName">程序集名称 例如：System.Data</param>
        /// <param name="fullName">对象的完整的名称 例如：System.Data.SqlClient.SqlConnection</param>
        /// <param name="args">构造函数的参数</param>
        /// <returns></returns>
        public static object CreateInstance(string assemblyName, string fullName, params object[] args)
        {

            Assembly ass = Assembly.Load(assemblyName);

            BindingFlags flags = (BindingFlags.NonPublic | BindingFlags.Public |
                BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            object obj = ass.CreateInstance(fullName, true, flags, null, args, null, null);
            return obj;
        }
        /// <summary>
        ///  根据type创建类的实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object CreateInstance(Type type)
        {
            return type.Assembly.CreateInstance(type.FullName);
        }
        /// <summary>
        ///  根据type创建类的实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object AssemblyLoad(string FullName)
        {
            return Assembly.Load(FullName);
        }
        /// <summary>
        /// 获取对象的所有属性
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties();
        }
        /// <summary>
        /// 获取类对应的属性
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TableAttribute GetTableAttribute(Type type)
        {
            object[] objs = type.GetCustomAttributes(typeof(TableAttribute), true);
            if (objs != null && objs.Length > 0)
                return (TableAttribute)objs[0];
            return null;
        }
        /// <summary>
        ///获取对象的所有Public或Instance变量
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static FieldInfo[] GetFields(Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic| BindingFlags.Public | BindingFlags.Instance);
            return fields;
        }

        /// <summary>
        /// 从DLL里的资源对象获取Stream
        /// </summary>
        /// <param name="assemblyName">程序集名称的长格式</param>
        /// <param name="resourceName">资源名称</param>
        /// <returns>资源对象Stream</returns>
        public static Stream GetResourceStream(string assemblyName, string resourceName)
        {
            Assembly ass = Assembly.Load(assemblyName);
            Stream stream = ass.GetManifestResourceStream(assemblyName + "." + resourceName);
            return stream;
        }

        /// <summary>
        /// 从DLL里获取XML文件
        /// </summary>
        /// <param name="assemblyName">程序集名称 </param>
        /// <param name="resourceName">资源的文件名称 例如:Strings.xml</param>
        /// <param name="name">节点名称</param>
        /// <returns>节点的InnerText</returns>
        public static XmlDocument GetResourceXML(string assemblyName, string resourceName)
        {
            Stream stream = GetResourceStream(assemblyName, resourceName);
            if (stream != null)
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(stream);

                return xmldoc;
            }
            return null;
        }

        
        /// <summary>
        /// 获取错误信息的字符串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static string GetString(string name)
        {
            Assembly ass = Assembly.GetAssembly(typeof(ReflectionHelper));
            Stream stream = ass.GetManifestResourceStream(typeof(ReflectionHelper), "");
            if (stream != null)
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(stream);
                XmlNode keyNode = xmldoc.SelectSingleNode("//" + name);

                return keyNode.InnerText;
            }

            return "";
        }

        /// <summary>
        /// 校验哪些项目不可以直接访问数据库
        /// </summary>
        /// <returns></returns>
        public static bool CheckCallingAssembly(Assembly callAssembly)
        {
            //AssemblyName assName = new AssemblyName(callAssembly.FullName);
            //string sz = ConfigurationHelper.GetSetting("RejectAccessDb");
            //if (sz == null)
            //{

            //    return true;
            //}
            //string[] rejectAssemblys = sz.Split(',');
            //for (int i = 0; i < rejectAssemblys.Length; i++)
            //{
            //    if (assName.Name.ToUpper() == rejectAssemblys[i].ToUpper())
            //    {
            //        throw new Exception(string.Format(GetString("S026"), assName.Name));
            //    }
            //}
            return true;
        }

        /// <summary>
        /// 通过名称获取实例(全路径)
        /// </summary>
        /// <param name="AssemblyName">实例名称</param>
        /// <returns></returns>
        public static object CreateInstance(string AssemblyName)
        {
            return CreateInstance(AssemblyName.Substring(0, AssemblyName.LastIndexOf(".")), AssemblyName,null);
        }
        public static  PropertyInfo[] GetPropertyInfo(string AssemblyName)
        {
            var obj = CreateInstance(AssemblyName);
            var type = obj.GetType();
            return type.GetProperties();
        }
    }
}
