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
using Microsoft.Extensions.Configuration;
using Moq;


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
    [InlineData(0, 1, 0)]
    [InlineData(1, 0, 1)]
    [InlineData(100, 5, 105.10)]
    [InlineData(1000.34829, 10, 1105.00)]
    [InlineData(1500.84829, 12, 1691.19)]
    [InlineData(50000.508291015, 24, 63487.37)]
    public async Task CalculatorShouldReturnSuccess(decimal valorInicial, int meses, float expected) {

        // Arrange
        var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/v0/calculajuros?valorInicial={valorInicial}&meses={meses}");

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
       
        using var responseStream = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<float>(responseStream);
        
        Assert.Equal(expected, result);
    }

}