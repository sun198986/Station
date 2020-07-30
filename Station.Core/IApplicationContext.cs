using ServiceReference;
using Station.AppSettings;

namespace Station.Core
{
    public interface IApplicationContext
    {
        UserInfo CurrentUser { get; set; }
    }
}