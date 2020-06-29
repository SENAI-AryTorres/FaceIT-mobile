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
    public class CandidatoService
    {
        public async Task<bool> AddCandidato(Candidato candidato)
        {
			try
			{
				HttpClient client = new HttpClient();
				client.BaseAddress = new Uri("http://faceitapi.azurewebsites.net/api");

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Security.Security.TokenValue);

				var json = JsonConvert.SerializeObject(candidato);
				var data = new StringContent(json, Encoding.UTF8, "application/json");
				var httpresponse = await client.PostAsync(client.BaseAddress + "/Candidato", data);
				if (httpresponse.IsSuccessStatusCode)
				{
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{

				throw ex;
			}
        }
    }
}
