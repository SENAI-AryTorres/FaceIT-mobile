using faceitapi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static faceitapi.Models.Pessoa;

namespace FaceIT.Service
{
    public class PessoaService
    { 
        public async Task<PessoaFisica> GetPessoaAsync(int ID)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Security.Security.TokenValue);
                HttpResponseMessage response = await client.GetAsync("https://faceitapi.azurewebsites.net/api/PessoaFisica/" + ID);

                if (response.IsSuccessStatusCode)
                {
                    var pessoa = JsonConvert.DeserializeObject<PessoaFisica>(await response.Content.ReadAsStringAsync());
                    return pessoa;
                }
                else
                {
                    return null;
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
