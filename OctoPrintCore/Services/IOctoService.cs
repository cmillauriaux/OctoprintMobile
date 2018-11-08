using OctoPrintCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OctoPrintCore.Services
{
    public interface IOctoService
    {
        Task<ApiVersion> GetApiVersionAsync();
        Task<ApiInfo> GetApiInfoAsync();
        Task<ConnexionSettings> GetConnectionAsync();
        Task<LoginResponse> LoginAsync(LoginRequest connexion);
        Task<bool> ConnectAsync(ConnexionRequest connexion);
        Task<bool> DisconnectAsync();
        Task<JobInformation> GetJobAsync();
    }
}
