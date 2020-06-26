using faceitapi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FaceIT.Service
{
    public class PropostaService
    {
        public async Task<bool> AddProposta(Proposta proposta)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://faceitapi.azurewebsites.net/api");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Security.Security.TokenValue);

                var json = JsonConvert.SerializeObject(proposta);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpresponse = await client.PostAsync(client.BaseAddress + "/Proposta", data);

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

        public async Task<bool> GetProposta(Action<IEnumerable<Proposta>> proposta)
        {
            try
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Security.Security.TokenValue);

                HttpResponseMessage response = await client.GetAsync("https://faceitapi.azurewebsites.net/api/Proposta");

                if (response.IsSuccessStatusCode)
                {
                    var lista = JsonConvert.DeserializeObject<Proposta>(await response.Content.ReadAsStringAsync());
                    proposta(lista);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
