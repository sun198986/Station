using ServiceReference;

namespace Station.Aop
{
    public interface IApplicationContext
    {
        UserInfo CurrentUser { get; set; }

        CompanyInfo CurrentCompany { get; set; }


        T As<T>() where T : class, IApplicationContext;

        T Get<T>(string name);

        void Set(string name, object value);
    }
}