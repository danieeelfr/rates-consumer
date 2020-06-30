﻿using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConsumerAPI.DTOs;

namespace ConsumerAPI.Calculator
{
    [Route("api/v0/")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private const string RATES_API_BASE_URL = "https://danielfr-softplan-rates-api.azurewebsites.net/api/v0/";
        private const string RATES_API_LOGIN = "valid-user@gmail.com";
        private const string RATES_API_PASSWORD = "12345";
        private readonly IHttpClientFactory _httpClient;
        public CalculatorController(IHttpClientFactory _httpClient)
        {
            this._httpClient = _httpClient;
        }

        [HttpGet("calculajuros")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<double>> CalculaJurosAsync([FromQuery] decimal valorInicial, [FromQuery] int meses)
        {
            var taxaJuros = await GetAsync("taxajuros");
            double valorFinal = (double)valorInicial * (Math.Pow(1 + (double)taxaJuros, (double)meses));
            var result = (TruncateValue(Decimal.Parse(valorFinal.ToString()))).ToString("0.00");
            return new JsonResult(result);
        }

        private async Task<decimal> GetAsync(string action)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{RATES_API_BASE_URL}{action}");
            var client = _httpClient.CreateClient();

            var token = await GetAccessTokenAsync();

            request.Headers.Add("Authorization", $"Bearer {token.accessToken}");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<decimal>(responseStream);
                return result;
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return await GetAsync("/auth");
                };

                var reason = response.ReasonPhrase;
                var code = response.StatusCode;

                throw new Exception(code + "-" + reason);
            }


        }

        private async ValueTask<AuthOutputDTO> GetAccessTokenAsync()
        {
            var loginInput = new { email = RATES_API_LOGIN, password = RATES_API_PASSWORD };
            
            var request = new HttpRequestMessage(HttpMethod.Post, $"{RATES_API_BASE_URL}auth");
            request.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(loginInput),
                                                System.Text.Encoding.UTF8, "application/json");

            var client = _httpClient.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<AuthOutputDTO>(responseStream);

                return result;
            }
            else
            {
                throw new Exception();
            }
        }

        private decimal TruncateValue(decimal value) 
        {
            return Math.Truncate(value * 100) / 100;
        }
        
    }

}