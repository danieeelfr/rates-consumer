﻿using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConsumerAPI.DTOs;
using System.Globalization;

namespace ConsumerAPI.Controllers.Calculator
{
    [Route("api/v0/")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private const string RATES_API_BASE_URL = "https://danielfr-softplan-rates-api.azurewebsites.net/api/v0/";
        private const string RATES_API_LOGIN = "valid-user@gmail.com";
        private const string RATES_API_PASSWORD = "12345";

        private const string AUTH_ACTION = "auth";

        private readonly IHttpClientFactory _httpClient;
        public CalculatorController(IHttpClientFactory _httpClient)
        {
            this._httpClient = _httpClient;
        }

        [HttpGet("calculajuros")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<float>> CalculaJurosAsync([FromQuery] decimal valorInicial, [FromQuery] int meses)
        {
            var interestRate = await GetInterestRateAsync();

            var calculated = (valorInicial * 
                             (decimal)(Math.Pow(1 + (double)interestRate, (double)meses)));

            var truncated = TruncateValue(calculated);

            return new JsonResult(truncated);
        }

        private async Task<double> GetInterestRateAsync()
        {
            var client = _httpClient.CreateClient();
            var authResult = await GetAccessTokenAsync();
            var request = PrepareInterestRateRequest(authResult.accessToken);
            
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var interestRate = await JsonSerializer.DeserializeAsync<double>(responseStream);
                return interestRate;
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    return await GetInterestRateAsync();

                throw new Exception(response.StatusCode + "-" + response.ReasonPhrase);
            }
        }

        private async ValueTask<AuthOutputDTO> GetAccessTokenAsync()
        {
            var client = _httpClient.CreateClient();
            var request = PrepareAuthRequest();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<AuthOutputDTO>(responseStream);
            }

            throw new Exception(response.StatusCode + "-" + response.ReasonPhrase);
        }

        private decimal TruncateValue(decimal value)
        {
            return Math.Truncate(value * 100) / 100;
        }

        private HttpRequestMessage PrepareInterestRateRequest(string accessToken) 
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{RATES_API_BASE_URL}taxajuros");
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            return request;
        }

        private HttpRequestMessage PrepareAuthRequest()
        {
            var ratesApiAuthData = new { email = RATES_API_LOGIN, password = RATES_API_PASSWORD };
            var request = new HttpRequestMessage(HttpMethod.Post, $"{RATES_API_BASE_URL}{AUTH_ACTION}");
            request.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(ratesApiAuthData),
                                                System.Text.Encoding.UTF8, "application/json");

            return request;
        }
    }
}