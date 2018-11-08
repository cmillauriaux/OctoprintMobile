using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OctoPrintCore.Services
{
    public interface IFirebaseAuth
    {
        Task<bool> Connect(string login, string password);
        Task<bool> Subscribe(string login, string password);
        Task<bool> Disconnect();
        Task<bool> IsConnected();
        Task<string> GetToken();
    }
}
