using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using ServiceContracts.DTO;
using StockAppWithxUnit.Models;
using StockAppWithxUnit.ServicesContracts;

namespace StockAppWithXUnit.Controllers
{
  [Route("[controller]")]
  public class TradeController : Controller
  {
    public readonly IStockService _stockService;
    public readonly IFinnhubService _finnhubService;
    public readonly TradingOptions _tradingOptions;

    public TradeController(IStockService stockService, IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions)
    {
        _finnhubService = finnhubService;
        _stockService = stockService;
        _tradingOptions = tradingOptions.Value;
    }

    [Route("/")]
    [Route("[action]/{stockSymbol?}")]
    public async Task<IActionResult> Index(string stockSymbol = "MSFT")
    {
      ViewBag.CurrentStock = stockSymbol;
      ViewBag.CurrentUrl = "~/Trade/Index";

      var companyProfile = await _finnhubService.GetCompanyProfile(stockSymbol);

      var stockPriceQuote = await _finnhubService.GetStockPriceQuote(stockSymbol);

      StockTrade stockTrade = new()
      {
        StockSymbol = stockSymbol,
        StockName = companyProfile["name"].ToString(),
        Price = Convert.ToDouble(stockPriceQuote["c"].ToString(), CultureInfo.InvariantCulture),
        Quantity = (uint)_tradingOptions.DefaultOrderQuantity
      };

      return View(stockTrade);
    }

    [Route("[action]")]
    public async Task<IActionResult> Orders()
    {
      ViewBag.CurrentUrl = "~/Trade/Orders";

      List<BuyOrderResponse> buyOrderResponses = await _stockService.GetBuyOrders();
      List<SellOrderResponse> sellOrderResponses = await _stockService.GetSellOrders();

      StockAppWithxUnit.Models.Orders orders = new Orders()
      {
        BuyOrders = buyOrderResponses,
        SellOrders = sellOrderResponses
      };

      return View(orders);
    }
    [Route("[action]")]
    [HttpPost]
    public async  Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
    {

      if (!ModelState.IsValid)
      {
        return RedirectToAction("Index");
      }

      await _stockService.CreateBuyOrder(buyOrderRequest);
      return RedirectToAction(nameof(Orders));
    }
    [Route("[action]")]
    [HttpPost]
    public async  Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
    {
      if (!ModelState.IsValid)
      {
        return RedirectToAction("Index");
      }

      await _stockService.CreateSellOrder(sellOrderRequest);
      return RedirectToAction(nameof(Orders));
    }

    public async Task<IActionResult> OrdersPDF()
    {
      StockAppWithxUnit.Models.Orders orders = new Orders()
      {
        BuyOrders = await _stockService.GetBuyOrders(),
        SellOrders = await _stockService.GetSellOrders()
      };

      return new ViewAsPdf("OrdersPDF", orders, ViewData)
      {
        PageMargins = new Margins(20, 20, 20, 20)
      };
    }
  }
}
