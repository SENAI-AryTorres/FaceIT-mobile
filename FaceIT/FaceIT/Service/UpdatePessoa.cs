using faceitapi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FaceIT.Service
{
    public class UpdatePessoa
    {
        public async Task UpdatePessoaFisicaAsync(PessoaFisica pf)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://faceitapi.azurewebsites.net/api");

                var json = JsonConvert.SerializeObject(pf);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpresponse = await client.PutAsync(client.BaseAddress + "/PessoaFisica", data);
                if (httpresponse.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao atualizar produto");
                }
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task UpdatePessoaJuridicaAsync(PessoaJuridica pf)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://faceitapi.azurewebsites.net/api");

                var json = JsonConvert.SerializeObject(pf);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpresponse = await client.PutAsync(client.BaseAddress + "/PessoaFisica", data);
                if (httpresponse.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao atualizar produto");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
