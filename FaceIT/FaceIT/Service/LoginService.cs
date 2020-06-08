using faceitapi.Models;
using faceitapi.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace FaceIT.Service
{
    public class LoginService
    {
        public async Task<bool> Logar(LoginGet login)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://faceitapi.azurewebsites.net/api");

                var json = JsonConvert.SerializeObject(login);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpresponse = await client.PostAsync(client.BaseAddress + "/Login", data);

                if (httpresponse.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }   
        }
    }
}
