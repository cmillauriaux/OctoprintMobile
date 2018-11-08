using Newtonsoft.Json;
using OctoPrintCore.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OctoPrintCore.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient client;

        public AccountService()
        {
            client = new HttpClient();
        }

        public async Task<Account> GetAccount(string id)
        {
            try
            {
                var uri = new Uri(string.Format(Constants.BASE_SERVER_URL + Constants.SERVER_API_GET_PROFILE, string.Empty));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", id);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Account>(content);
                    return result;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public async Task<bool> registerFCMToken(string id, string token)
        {
            try
            {
                var uri = new Uri(string.Format(Constants.BASE_SERVER_URL + Constants.SERVER_API_REGISTER_FCM_TOKEN, string.Empty));

                var json = JsonConvert.SerializeObject(new Account() { Id = id, FCMToken = token });
                var payload = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, payload);
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

        public async Task<bool> unregisterFCMToken(string id)
        {
            try
            {
                var uri = new Uri(string.Format(Constants.BASE_SERVER_URL + Constants.SERVER_API_UNREGISTER_FCM_TOKEN, string.Empty));

                var json = JsonConvert.SerializeObject(new Account() { Id = id });
                var payload = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, payload);
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
    }
}
