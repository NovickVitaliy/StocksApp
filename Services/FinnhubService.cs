using System.Text.Json;
using ServiceContracts;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;

namespace Services
{
  public class FinnhubService : IFinnhubService
  {
    public readonly IHttpClientFactory _httpClientFactory;
    public readonly IConfiguration _configurations;
    public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configurations)
    {
      _httpClientFactory = httpClientFactory;
      _configurations = configurations;
    }

    public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
    {
      string token = _configurations.GetValue<string>("finnhubtoken");

      using (HttpClient httpClient = _httpClientFactory.CreateClient())
      {
        HttpRequestMessage request = new HttpRequestMessage()
        {
          RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={token}"),
          Method = HttpMethod.Get
        };

        HttpResponseMessage response = await httpClient.SendAsync(request);

        string json = await response.Content.ReadAsStringAsync();

        Dictionary<string, object>? dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(json);


        return dictionary;
      }
    }

    public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
    {
      string token = _configurations.GetValue<string>("finnhubtoken");

      using (HttpClient httpClient = _httpClientFactory.CreateClient())
      {
        HttpRequestMessage request = new HttpRequestMessage()
        {
          RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={token}"),
          Method = HttpMethod.Get
        };

        HttpResponseMessage response = await httpClient.SendAsync(request);

        string json = await response.Content.ReadAsStringAsync();

        Dictionary<string, object>? dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(json);


        return dictionary;
      }
    }
  }
}