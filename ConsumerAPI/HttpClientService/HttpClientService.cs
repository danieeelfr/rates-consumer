using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumerAPI.HttpClient
{
    public class HttpClientService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly HttpClientService _service;

        public HttpClientService(IHttpClientFactory _httpClient)
        {
            this._httpClient = _httpClient;
        }

        // public async Task OnGet()
        // {
        //     var request = new HttpRequestMessage(HttpMethod.Get,
        //         "https://danielfr-softplan-rates-api.azurewebsites.net/api/v0/taxajuros");

        //         string token = "";

        //     request.Headers.Add("Authorization", $"Bearer {token}");
        //     // request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

        //     var client = _httpClient.CreateClient();

        //     var response = await client.SendAsync(request);

        //     if (response.IsSuccessStatusCode)
        //     {
        //         using var responseStream = await response.Content.ReadAsStreamAsync();
        //         var result = await JsonSerializer.DeserializeAsync
        //             <decimal>(responseStream);
        //     }
        //     else
        //     {
        //         var reason = response.ReasonPhrase;
        //         var code = response.StatusCode;
        //     }
        // }

    }
}