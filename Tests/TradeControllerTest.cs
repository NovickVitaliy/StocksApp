using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Rotativa.AspNetCore;
using ServiceContracts.DTO;
using StockAppWithxUnit.Models;
using StockAppWithxUnit.ServicesContracts;
using StockAppWithXUnit.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
  public class TradeControllerTest
  {
    private readonly IStockService _stockService;
    private readonly IFinnhubService _finnhubService;
    private readonly Mock<IFinnhubService> _mockFinnhub;
    private readonly Mock<IStockService> _mockStock;
    private readonly IFixture _fixture;

    public TradeControllerTest()
    {
      _fixture = new Fixture();

      _mockFinnhub = new Mock<IFinnhubService>();
      _mockStock = new Mock<IStockService>();

      _stockService = _mockStock.Object;
      _finnhubService = _mockFinnhub.Object;
    }

    [Fact]
    public async Task Index_ToReturnValidView()
    {
      TradeController tradeController = new TradeController(_stockService, _finnhubService, null);

      _mockFinnhub.Setup(item => item.GetCompanyProfile(It.IsAny<string>()))
        .ReturnsAsync(new Dictionary<string, object>()
        {
          {"name","pidor" }
        });
      _mockFinnhub.Setup(item => item.GetStockPriceQuote(It.IsAny<string>()))
        .ReturnsAsync(new Dictionary<string, object>() { { "c", 50 } });

      var result = await tradeController.Index();

      result.Should().BeOfType(typeof(ViewResult));

      var viewResult = (ViewResult) result;

      viewResult.ViewData.Model.Should().BeOfType(typeof(StockTrade));
    }

    [Fact]
    public async Task Orders_ToBeCorrect()
    {
      TradeController tradeController = new TradeController(_stockService, _finnhubService, null);

      _mockStock.Setup(i => i.GetBuyOrders()).ReturnsAsync(new List<BuyOrderResponse>());
      _mockStock.Setup(i => i.GetSellOrders()).ReturnsAsync(new List<SellOrderResponse>());

      IActionResult actionResult = await tradeController.Orders();
      ViewResult viewResult = (ViewResult) actionResult;

      actionResult.Should().NotBeNull();
      actionResult.Should().BeOfType(typeof(ViewResult));
      viewResult.ViewData.Model.Should().BeOfType(typeof(Orders));
    }

    [Fact]
    public async Task OrdersPDF_CorrectWork()
    {
      TradeController tradeController = new TradeController(_stockService, _finnhubService, null);

      _mockStock.Setup(i => i.GetBuyOrders()).ReturnsAsync(new List<BuyOrderResponse>());
      _mockStock.Setup(i => i.GetSellOrders()).ReturnsAsync(new List<SellOrderResponse>());

      IActionResult actionResult = await tradeController.OrdersPDF();

      actionResult.Should().BeOfType(typeof(ViewAsPdf));

      ViewAsPdf reslutAsPdf = (ViewAsPdf) actionResult;
       
    }
  }
}
