using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockAppWithxUnit.Models;
using StockAppWithxUnit.ServicesContracts;

namespace StockAppWithxUnit.Controllers
{
  [Route("[controller]")]
  public class StockController : Controller
  {
    private readonly IFinnhubService _finnhubService;
    private readonly IConfiguration _configuration;
    private readonly TradingOptions _tradingOptions;
    public StockController(IFinnhubService finnhubService, IConfiguration configuration, IOptions<TradingOptions> tradingOptions)
    {
      _configuration = configuration;
      _finnhubService = finnhubService;
      _tradingOptions = tradingOptions.Value;
    }

    [HttpGet]
    [Route("[action]/{stockSymbol?}")]
    public async Task<IActionResult> Explore(string stockSymbol)
    {
      ViewBag.CurrentUrl = "Stock/Explore";

      List<Dictionary<string, object>> info = await _finnhubService.GetStocks();
      List<string> top25Companies = _tradingOptions.Top25PopularStocks.Split(',').ToList();

      List<Stock> stocks = new List<Stock>();

      foreach (var stock in info)
      {
        if(stock.ContainsKey("displaySymbol"))
        {
          if (top25Companies.Any(name => name.Equals(stock["displaySymbol"].ToString())))
          {
            if (stock["displaySymbol"].ToString() == stockSymbol)
            {
              var stockInfo = await _finnhubService.GetCompanyProfile(stockSymbol);

              stockInfo.Add(
                key: "price",
                value: (await _finnhubService.GetStockPriceQuote(stockSymbol))["c"].ToString()
                );
              ViewBag.CurrentStockInfo = stockInfo;
            }
            stocks.Add(new Stock
            {
              StockName = stock["description"].ToString(),
              StockSymbol = stock["displaySymbol"].ToString()
            });
          }
        }
      }

      return View(stocks);
    }
  }
}
