using Xunit;
using ConsumerAPI;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System;
using Newtonsoft.Json;
using ConsumerAPI.DTOs;

public class ShowMeControllerIntegrationTests {

    private readonly HttpClient _client;

    public ShowMeControllerIntegrationTests()
    {
        var server = new TestServer(new WebHostBuilder()
        .UseEnvironment("Development")
        .UseStartup<Startup>());

        _client = server.CreateClient();
    }

    [Fact]
    public async Task ShowMeShouldReturnRepositoriesWithSuccess() {

        // Arrange
        var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/v0/showmethecode");

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        string responseBody = await response.Content.ReadAsStringAsync();

        var obj = JsonConvert.DeserializeObject<ShowMeOutputDTO>(responseBody);
       
        Assert.Equal("https://danielfr-softplan-rates-api.azurewebsites.net/index.html", obj.urls[0]);
        Assert.Equal("https://danielfr-softplan-consumer-api.azurewebsites.net/index.html", obj.urls[1]);
        
    }
}