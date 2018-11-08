using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OctoPrintCore.Services
{
    public interface IFirebaseNotificationsService
    {
        Task<string> Register(string id);
        Task Unregister(string id);
    }
}
