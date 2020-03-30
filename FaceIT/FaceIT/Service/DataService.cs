using faceitapi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FaceIT.Service
{
    public class DataService
    {
        HttpClient client = new HttpClient();

        public async Task<List<Pessoa>> GetPessoaAsync()
        {
            try
            {
                string url = "https://faceitapi.azurewebsites.net/api/PessoaFisica";
                var response = await client.GetStringAsync(url);
                var pessoa = JsonConvert.DeserializeObject<List<Pessoa>>(response);
                return pessoa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
