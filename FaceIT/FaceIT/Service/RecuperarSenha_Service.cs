using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FaceIT.Service
{
    public class RecuperarSenha_Service
    {
        public async Task<bool> Recuperar(string email)
        {
			try
			{
				HttpClient client = new HttpClient();
				client.BaseAddress = new Uri("https://faceitapi.azurewebsites.net/api");

				var json = JsonConvert.SerializeObject(email);
				var dados = new StringContent(json, Encoding.UTF8, "application/json");
				var httpresponse = await client.PostAsync(client.BaseAddress + "/RecuperarSenha", dados);

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
