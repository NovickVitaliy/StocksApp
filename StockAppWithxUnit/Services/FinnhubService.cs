using System.Text.Json;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using StockAppWithxUnit.RepositoriesContracts;
using StockAppWithxUnit.ServicesContracts;

namespace Services
{
  public class FinnhubService : IFinnhubService
  {
    private readonly IFinnhubRepository _finnhubRepository;
    public FinnhubService(IFinnhubRepository finnhubRepository)
    {
      _finnhubRepository = finnhubRepository;
    }

    public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
    {
      return await _finnhubRepository.GetCompanyProfile(stockSymbol);
    }

    public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
    {
      return await _finnhubRepository.GetStockPriceQuote(stockSymbol);
    }

    public async Task<List<Dictionary<string, object>>?> GetStocks()
    {
      return await _finnhubRepository.GetStocks();
    }

    public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
    {
      return await _finnhubRepository.SearchStocks(stockSymbolToSearch);
    }
  }
}