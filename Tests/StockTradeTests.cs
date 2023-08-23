using AutoFixture;
using Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using StockAppWithxUnit.Entities;
using StockAppWithxUnit.RepositoriesContracts;
using StockAppWithxUnit.ServicesContracts;

namespace ProjectUnitTests
{
  public class StockTradeTests
  {
    private readonly IStockService _stockService;
    private readonly Fixture _fixture;
    private readonly Mock<IStocksRepository> _stocksRepositoryMock;
    private readonly IStocksRepository _stocksRepository;

    public StockTradeTests()
    {
      _fixture = new Fixture();

      _stocksRepositoryMock = new Mock<IStocksRepository>();
      _stocksRepository = _stocksRepositoryMock.Object;

      _stockService = new StockService(_stocksRepository);
    }

    #region CreateBuyOrder

    [Fact]
    public void CreateBuyOrder_BuyOrderRequestIsNull()
    {
      BuyOrderRequest? request = null;

      Assert.ThrowsAsync<ArgumentNullException>(() => _stockService.CreateBuyOrder(request));
    }

    [Fact]
    public void CreateBuyOrder_BuyOrderQuantityIsLessThanPossible()
    {
      BuyOrderRequest request = new BuyOrderRequest()
      {
        Quantity = 0
      };

      Assert.ThrowsAsync<ArgumentException>(() => _stockService.CreateBuyOrder(request));
    }

    [Fact]
    public void CreatebuyOrder_BuyOrderQuantityIsHigherThanPossible()
    {
      BuyOrderRequest request = new()
      {
        Quantity = 100001
      };

      Assert.ThrowsAsync<ArgumentException>(() => _stockService.CreateBuyOrder(request));
    }

    [Fact]
    public void CreateBuyOrder_BuyOrderPriceIsLessThanPossible()
    {
      BuyOrderRequest request = new()
      {
        Price = 0
      };

      Assert.ThrowsAsync<ArgumentException>(() => _stockService.CreateBuyOrder(request));
    }

    [Fact]
    public void CreateBuyOrder_BuyOrderPriceIsHigherThanPossible()
    {
      BuyOrderRequest request = new()
      {
        Price = 10001
      };

      Assert.ThrowsAsync<ArgumentException>(() => _stockService.CreateBuyOrder(request));
    }

    [Fact]
    public void CreateBuyOrder_StockSymbolIsNull()
    {
      BuyOrderRequest request = new()
      {
        StockSymbol = null
      };

      Assert.ThrowsAsync<ArgumentException>(() => _stockService.CreateBuyOrder(request));
    }

    [Fact]
    public void CreateBuyOrder_DateAndTimeOfOrderIsLessThanItShouldBeForTheRealestMostFuckingShitInTheWholeBloodyWorld()
    {
      BuyOrderRequest request = new()
      {
        DateAndTimeOfOrder = DateTime.Parse("1999-12-31")
      };

      Assert.ThrowsAsync<ArgumentException>(() => _stockService.CreateBuyOrder(request));
    }

    [Fact]
    public async void CreateBuyOrder_OrderCreatedSuccessfully()
    {
      BuyOrderRequest buyOrderRequest = _fixture.Create<BuyOrderRequest>();

      BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

      BuyOrderResponse buyOrderResponseExpected = buyOrder.ToBuyOrderResponse();

      _stocksRepositoryMock.Setup(item => item.CreateBuyOrder(It.IsAny<BuyOrder>()))
        .ReturnsAsync(buyOrder);

      var buyOrderResponseActual = await _stockService.CreateBuyOrder(buyOrderRequest);

      buyOrderResponseExpected.BuyOrderID = buyOrderResponseActual.BuyOrderID;

      buyOrderResponseActual.BuyOrderID.Should().NotBe(Guid.Empty);

      buyOrderResponseActual.Should().Be(buyOrderResponseExpected);
    }

    #endregion

    #region CreateSellOrder

    [Fact]
    public void CreateSellOrder_SellOrderRequestIsNull()
    {
      SellOrderRequest? request = null;

      Assert.ThrowsAsync<ArgumentNullException>(() => _stockService.CreateSellOrder(request));
    }

    [Fact]
    public void CreateSellOrder_SellOrderQuantityIsLessThanPossible()
    {
      SellOrderRequest request = new SellOrderRequest()
      {
        Quantity = 0
      };

      Assert.ThrowsAsync<ArgumentException>(() => _stockService.CreateSellOrder(request));
    }

    [Fact]
    public void CreateSellOrder_SellOrderQuantityIsHigherThanPossible()
    {
      SellOrderRequest request = new()
      {
        Quantity = 100001
      };

      Assert.ThrowsAsync<ArgumentException>(() => _stockService.CreateSellOrder(request));
    }

    [Fact]
    public void CreateSellOrder_SellOrderPriceIsLessThanPossible()
    {
      SellOrderRequest request = new()
      {
        Price = 0
      };

      Assert.ThrowsAsync<ArgumentException>(() => _stockService.CreateSellOrder(request));
    }

    [Fact]
    public void CreateSellOrder_SellOrderPriceIsHigherThanPossible()
    {
      SellOrderRequest request = new()
      {
        Price = 10001
      };

      Assert.ThrowsAsync<ArgumentException>(() => _stockService.CreateSellOrder(request));
    }

    [Fact]
    public void CreateSellOrder_StockSymbolIsNull()
    {
      SellOrderRequest request = new()
      {
        StockSymbol = null
      };

      Assert.ThrowsAsync<ArgumentException>(() => _stockService.CreateSellOrder(request));
    }

    [Fact]
    public void CreateSellOrder_DateAndTimeOfOrderIsLessThanItShouldBeForTheRealestMostFuckingShitInTheWholeBloodyWorld()
    {
      SellOrderRequest request = new()
      {
        DateAndTimeOfOrder = DateTime.Parse("1999-12-31")
      };

      Assert.ThrowsAsync<ArgumentException>(() => _stockService.CreateSellOrder(request));
    }

    [Fact]
    public async void CreateSellOrder_OrderCreatedSuccessfully()
    {
      SellOrderRequest sellOrderRequest = new()
      {
        DateAndTimeOfOrder = DateTime.Parse("2005-04-25"),
        Price = 1488,
        Quantity = 1337,
        StockName = "Apple",
        StockSymbol = "AAPL",
      };

      var sellOrder = sellOrderRequest.ToSellOrder();

      var sellOrderResponseExpected = sellOrder.ToSellOrderResponse();

      _stocksRepositoryMock.Setup(item => item.CreateSellOrder(It.IsAny<SellOrder>()))
        .ReturnsAsync(sellOrder);

      var sellOrderResponseActual = await _stockService.CreateSellOrder(sellOrderRequest);

      sellOrderResponseExpected.SellOrderId = sellOrderResponseActual.SellOrderId;

      sellOrderResponseActual.Should().Be(sellOrderResponseExpected);

      sellOrderResponseActual.SellOrderId.Should().NotBe(Guid.Empty);

    }

    #endregion

    #region GetAllBuyOrders

    [Fact]
    public async void GetAllBuyOrders_DefaultEmptyList()
    {
      List<BuyOrder> listBuyOrders = new List<BuyOrder>();


      _stocksRepositoryMock.Setup(item => item.GetBuyOrders())
        .ReturnsAsync(listBuyOrders);


      List<BuyOrderResponse> listOfBuyOrderResponseActual = await _stockService.GetBuyOrders();

      listOfBuyOrderResponseActual.Should().BeEmpty();
    }

    [Fact]
    public async void GetAllBuyOrders_CorrectWorkOfMethod()
    {
      List<BuyOrder> buyOrders = new List<BuyOrder>()
      {
        _fixture.Create<BuyOrder>(),
        _fixture.Create<BuyOrder>(),
        _fixture.Create<BuyOrder>(),
      };

      _stocksRepositoryMock.Setup(item => item.GetBuyOrders())
        .ReturnsAsync(buyOrders);

      var buyOrdersListExpected = buyOrders.Select(item => item.ToBuyOrderResponse()).ToList();  
     
      var buyOrdersListActual = await _stockService.GetBuyOrders();

      buyOrdersListActual.Should().BeEquivalentTo(buyOrdersListExpected);
    }

    #endregion

    #region GetSellOrders

    [Fact]
    public async void GetSellOrders_DefaultEmptyList()
    {
      List<SellOrder> sellOrders = new List<SellOrder>();

      _stocksRepositoryMock.Setup(item => item.GetSellOrders())
        .ReturnsAsync(sellOrders);

      var actualList = await _stockService.GetSellOrders();

      actualList.Should().BeEmpty();
    }

    [Fact]
    public async void GetSellOrders_CorrectWorkOfMethod()
    {
      List<SellOrder> sellOrders = new List<SellOrder>()
      {
        _fixture.Create<SellOrder>(),
        _fixture.Create<SellOrder>(),
        _fixture.Create<SellOrder>()
      };

      var expectedList = sellOrders.Select(item => item.ToSellOrderResponse()).ToList();

      _stocksRepositoryMock.Setup(item => item.GetSellOrders())
        .ReturnsAsync(sellOrders);

      var actualList = await _stockService.GetSellOrders();

      actualList.Should().BeEquivalentTo(expectedList);
    }
    #endregion
  }
}