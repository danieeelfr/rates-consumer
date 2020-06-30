using Xunit;
using ConsumerAPI;
using ConsumerAPI.DTOs;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text.Json;

public class CalculatorControllerIntegrationTests {

    private readonly HttpClient _client;

    public CalculatorControllerIntegrationTests()
    {
        var server = new TestServer(new WebHostBuilder()
        .UseEnvironment("Development")
        .UseStartup<Startup>());

        _client = server.CreateClient();
    }

    [Theory]
    [InlineData(100.0, 5)]
    public async Task CalculatorShouldReturnSuccess(decimal valorInicial, int meses) {

        // Arrange
        var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/v0/calculajuros?valorInicial={valorInicial}&meses={meses}");

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
       
        using var responseStream = await response.Content.ReadAsStreamAsync();
        string result = await JsonSerializer.DeserializeAsync<string>(responseStream);
        
        Assert.Equal("105.10", result.ToString());
    }

}