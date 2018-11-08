using OctoPrintCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OctoPrintCore.Services
{
    public interface IAccountService
    {
        Task<bool> registerFCMToken(string id, string token);
        Task<bool> unregisterFCMToken(string id);
        Task<Account> GetAccount(string id);
    }
}
