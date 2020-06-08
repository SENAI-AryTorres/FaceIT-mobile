using faceitapi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FaceIT.Service
{
    public class SkillService
    {
        public static async Task GetSkillAsync(Action<IEnumerable<Skill>> action)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://faceitapi.azurewebsites.net/api/Skill");

            if(response.StatusCode == System.Net.HttpStatusCode.OK) {
                var lista = JsonConvert.DeserializeObject<IEnumerable<Skill>>(await response.Content.ReadAsStringAsync());
                action(lista);
            }
        }
    }
}
