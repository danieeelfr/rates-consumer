using Xunit;
using ConsumerAPI;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

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
        string responseBody = await response.Content.ReadAsStringAsync();
       
       Debug.WriteLine(responseBody);
        
    }

}