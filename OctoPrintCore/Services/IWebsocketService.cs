using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OctoPrintCore.Services
{
    public interface IWebsocketService
    {
        Task Connect();
    }
}
