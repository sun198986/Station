using System.Threading.Tasks;
using ServiceReference;

namespace Station.Repository.User
{
    public interface IUserRepository
    {
        Task<UserInfo> Login(string userName, string pwd);
    }
}