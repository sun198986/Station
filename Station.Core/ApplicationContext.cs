using ServiceReference;
using Station.AppSettings;

namespace Station.Core
{
    public class ApplicationContext:IApplicationContext
    {
        public UserInfo CurrentUser { get; set; }
    }
}