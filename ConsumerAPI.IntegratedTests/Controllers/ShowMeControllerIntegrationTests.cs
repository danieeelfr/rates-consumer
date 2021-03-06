﻿using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ConsumerAPI.DTOs;

namespace ConsumerAPI.IntegratedTests.Controllers
{
    public class ShowMeControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public ShowMeControllerIntegrationTests()
        {
            var server = new TestServer(new WebHostBuilder()
            .UseEnvironment("Development")
            .UseStartup<Startup>());

            _client = server.CreateClient();
        }

        [Fact]
        public async Task ShowMeShouldReturnRepositoriesWithSuccess()
        {

            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/v0/showmethecode");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ShowMeOutputDTO>(responseBody);

            Assert.Equal("https://github.com/danieeelfr/rates-service", obj.urls[0]);
            Assert.Equal("https://github.com/danieeelfr/rates-consumer", obj.urls[1]);

        }
    }
}