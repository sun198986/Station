using System;
using System.Threading.Tasks;

namespace Station.Repository.Token
{
    public interface ITokenRepository
    {
        Task<string> GetToken(string userName, DateTime startDateTime, DateTime endDateTime);
    }
};