using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using StockAppWithxUnit.RepositoriesContracts;
using System.Text.Json;

namespace StockAppWithxUnit.RepositoriesImplementations
{
  public class FinnhubRepository : IFinnhubRepository
  {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configurations;
    public FinnhubRepository(IHttpClientFactory httpClientFactory, IConfiguration configurations)
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

        string json = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();

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

        string json = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();


        Dictionary<string, object>? dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(json);


        return dictionary;
      }
    }

    public async Task<List<Dictionary<string, object>>?> GetStocks()
    {
      string token = _configurations.GetValue<string>("finnhubtoken");

      using (var client = _httpClientFactory.CreateClient())
      {
        HttpRequestMessage requestMessage = new HttpRequestMessage() 
        {
          RequestUri = new Uri($"https://finnhub.io/api/v1/stock/symbol?exchange=US&token={token}"),
          Method = HttpMethod.Get
        };

        HttpResponseMessage response = await client.SendAsync(requestMessage);

        string json = await new StreamReader(await response.Content.ReadAsStreamAsync()).ReadToEndAsync();

        List<Dictionary<string, object>>? info = JsonSerializer.Deserialize<List<Dictionary<string, object>>?>(json);

        return info;
      }
    }

    public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
    {
      string token = _configurations.GetValue<string>("finnhubtoken");

      using (var client = _httpClientFactory.CreateClient())
      {
        HttpRequestMessage requestMessage = new HttpRequestMessage()
        {
          RequestUri = new Uri($"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={token}"),
          Method = HttpMethod.Get
        };

        HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);

        string json = await new StreamReader(await responseMessage.Content.ReadAsStreamAsync()).ReadToEndAsync();

        Dictionary<string, object>? info = JsonSerializer.Deserialize<Dictionary<string, object>?>(json);

        return info;
      }
    }
  }
}
