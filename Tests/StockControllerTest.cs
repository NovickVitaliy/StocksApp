using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using StockAppWithxUnit.Controllers;
using StockAppWithxUnit.Models;
using StockAppWithxUnit.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
  public class StockControllerTest
  {
    private readonly IFinnhubService _finnhubService;
    private readonly Mock<IOptions<TradingOptions>> _tradeOptionsMock;
    private readonly Mock<IFinnhubService> _finnhubServiceMock;
    private readonly IFixture _fixture;
    public StockControllerTest()
    {
      _fixture = new Fixture();

      _finnhubServiceMock = new Mock<IFinnhubService>();
      _tradeOptionsMock = new Mock<IOptions<TradingOptions>>();

      _finnhubService = _finnhubServiceMock.Object;
    }
    [Fact]
    public async Task Eplore_CorrectWork()
    {
      _tradeOptionsMock.Setup(i => i.Value).Returns(new TradingOptions()
      {
        Top25PopularStocks = "",
        DefaultOrderQuantity = 1,
      });

      _finnhubServiceMock.Setup(i => i.GetStocks())
        .ReturnsAsync(new List<Dictionary<string, object>>()
        {
          new Dictionary<string, object>()
          {
            {"displaySymbol", "loh" },
            {"description", "pidor" }
          }
        });

      _finnhubServiceMock.Setup(i => i.GetCompanyProfile(It.IsAny<string>()))
        .ReturnsAsync(new Dictionary<string, object>());

      _finnhubServiceMock.Setup(i => i.GetStockPriceQuote(It.IsAny<string>()))
        .ReturnsAsync(new Dictionary<string, object>()
        {
          {"c", 6 }
        });

      StockController stockController = new StockController(_finnhubService,null, _tradeOptionsMock.Object);

      IActionResult actionResult = await stockController.Explore(It.IsAny<string>());

      ViewResult viewResult = (ViewResult)actionResult;

      actionResult.Should().BeOfType(typeof(ViewResult));

      viewResult.ViewData.Model.Should().BeOfType(typeof(List<Stock>));
    }
  }
}
