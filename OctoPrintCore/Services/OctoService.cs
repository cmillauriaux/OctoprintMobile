using Newtonsoft.Json;
using OctoPrintCore.Model;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OctoPrintCore.Services
{
    public class OctoService : IOctoService
    {
        private HttpClient client;

        public OctoService()
        {
            client = new HttpClient();
            //client.MaxResponseContentBufferSize = 256000;
            if (!string.IsNullOrEmpty(UserProperties.GetApiKey()))
            {
                client.DefaultRequestHeaders.Add("X-Api-Key", UserProperties.GetApiKey());
            }
        }

        public async Task<ApiVersion> GetApiVersionAsync()
        {
            try
            {
                var uri = new Uri(string.Format(Constants.BASE_URL + UserProperties.GetLocalIP() + Constants.API_VERSION, string.Empty));
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ApiVersion>(content);
                    return result;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public async Task<ApiInfo> GetApiInfoAsync()
        {
            try
            {
                var uri = new Uri(string.Format(Constants.BASE_URL + UserProperties.GetLocalIP() + Constants.API_VERSION, string.Empty));
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ApiInfo>(content);
                    return result;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public async Task<ConnexionSettings> GetConnectionAsync()
        {
            try
            {
                var uri = new Uri(string.Format(Constants.BASE_URL + UserProperties.GetLocalIP() + Constants.API_CONNECTION, string.Empty));
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ConnexionSettings>(content);
                    return result;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest connexion)
        {
            try
            {
                if (!string.IsNullOrEmpty(UserProperties.GetApiKey()) && !client.DefaultRequestHeaders.Contains("X-Api-Key"))
                {
                    client.DefaultRequestHeaders.Add("X-Api-Key", UserProperties.GetApiKey());
                }

                var uri = new Uri(string.Format(Constants.BASE_URL + UserProperties.GetLocalIP() + Constants.API_LOGIN, string.Empty));

                var json = JsonConvert.SerializeObject(connexion);
                var payload = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, payload);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginResponse>(content);
                    return result;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public async Task<bool> ConnectAsync(ConnexionRequest connexion)
        {
            try
            {

                connexion.command = "connect";
                var uri = new Uri(string.Format(Constants.BASE_URL + UserProperties.GetLocalIP() + Constants.API_CONNECT, string.Empty));

                var json = JsonConvert.SerializeObject(connexion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return false;
        }

        public async Task<bool> DisconnectAsync()
        {
            try
            {
                ConnexionRequest connexion = new ConnexionRequest() { command = "disconnect" };
                var uri = new Uri(string.Format(Constants.BASE_URL + UserProperties.GetLocalIP() + Constants.API_DISCONNECT, string.Empty));

                var json = JsonConvert.SerializeObject(connexion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return false;
        }

        public async Task<JobInformation> GetJobAsync()
        {
            try
            {
                var uri = new Uri(string.Format(Constants.BASE_URL + UserProperties.GetLocalIP() + Constants.API_JOB, string.Empty));
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<JobInformation>(content);
                    return result;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
    }
}
