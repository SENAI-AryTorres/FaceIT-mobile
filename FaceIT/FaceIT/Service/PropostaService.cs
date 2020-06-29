using faceitapi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FaceIT.Service
{
    public class PropostaService
    {
        private List<Proposta> _proposta;
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

        public async Task<List<Proposta>> GetPropostaAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Security.Security.TokenValue);
                HttpResponseMessage response = await client.GetAsync("https://faceitapi.azurewebsites.net/api/Proposta");

                if (response.IsSuccessStatusCode)
                {
                    var lista = JsonConvert.DeserializeObject<List<Proposta>>(await response.Content.ReadAsStringAsync());
                    return lista;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<List<Proposta>> GetPropostabyID(int ID)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Security.Security.TokenValue);
                HttpResponseMessage response = await client.GetAsync("https://faceitapi.azurewebsites.net/api/Proposta/PropostasEmpresa/" + ID);

                if (response.IsSuccessStatusCode)
                {
                    var lista = JsonConvert.DeserializeObject<List<Proposta>>(await response.Content.ReadAsStringAsync());
                    return lista;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
