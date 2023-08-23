namespace StockAppWithxUnit.ServicesContracts
{
  public interface IFinnhubService
  {
    Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
    Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);

    Task<List<Dictionary<string, object>>?> GetStocks();
    Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
  }
}