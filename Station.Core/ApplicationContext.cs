using ServiceReference;

namespace Station.Core
{

    public class ApplicationContext:IApplicationContext
    {
        public UserInfo CurrentUser { get; set; }
        public CompanyInfo CurrentCompany { get; set; }

        public T As<T>() where T : class, IApplicationContext
        {
            throw new System.NotImplementedException();
        }

        public T Get<T>(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Set(string name, object value)
        {
            throw new System.NotImplementedException();
        }
    }
}