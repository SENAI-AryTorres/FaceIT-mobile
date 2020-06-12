using faceitapi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FaceIT.Service
{
    public class Cadastro_Pessoa_Fisica
    {
        public async Task<bool> AddPessoaFisica(PessoaFisica pessoa)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://faceitapi.azurewebsites.net/api");

                var json = JsonConvert.SerializeObject(pessoa);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpresponse = await client.PostAsync(client.BaseAddress + "/PessoaFisica", data);

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

        public async Task<bool> AddPessoaJuridica(PessoaJuridica pessoa)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://faceitapi.azurewebsites.net/api");

                var json = JsonConvert.SerializeObject(pessoa);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpresponse = await client.PostAsync(client.BaseAddress + "/PessoaJuridica", data);

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
