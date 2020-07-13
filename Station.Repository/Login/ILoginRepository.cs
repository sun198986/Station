using System;
using System.Threading.Tasks;
using ServiceReference;

namespace Station.Repository.Login
{
    public interface ILoginRepository
    {
        Task<UserInfo> Login(string userName,string pwd);

        Task<string> GetToken(string userName,DateTime startDateTime,DateTime endDateTime);
    }
}