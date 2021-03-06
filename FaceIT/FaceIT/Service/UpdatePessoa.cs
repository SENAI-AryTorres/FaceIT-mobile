﻿using faceitapi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FaceIT.Service
{
    public class UpdatePessoa
    {
        public async Task<bool> UpdatePessoaFisicaAsync(PessoaFisica pf)
        {
            try
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Security.Security.TokenValue);

                client.BaseAddress = new Uri("https://faceitapi.azurewebsites.net/api");

                var json = JsonConvert.SerializeObject(pf);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpresponse = await client.PutAsync(client.BaseAddress + "/PessoaFisica", data);

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

        public async Task<bool> UpdatePessoaJuridicaAsync(PessoaJuridica pj)
        {
            try
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Security.Security.TokenValue);

                client.BaseAddress = new Uri("https://faceitapi.azurewebsites.net/api");

                var json = JsonConvert.SerializeObject(pj);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpresponse = await client.PutAsync(client.BaseAddress + "/PessoaJuridica", data);

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
