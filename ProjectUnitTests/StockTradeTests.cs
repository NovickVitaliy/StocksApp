using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace ProjectUnitTests
{
  public class StockTradeTests
  {
    private readonly IStockService _stockService;

    public StockTradeTests()
    {
      _stockService = new StockService();
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
      BuyOrderRequest request = new()
      {
        DateAndTimeOfOrder = DateTime.Parse("2005-04-25"),
        Price = 1488,
        Quantity = 1337,
        StockName = "Apple",
        StockSymbol = "AAPL"
      };

      BuyOrderResponse response = await _stockService.CreateBuyOrder(request);

      Assert.True(response.BuyOrderID != Guid.Empty);
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
      SellOrderRequest request = new()
      {
        DateAndTimeOfOrder = DateTime.Parse("2005-04-25"),
        Price = 1488,
        Quantity = 1337,
        StockName = "Apple",
        StockSymbol = "AAPL"
      };

      SellOrderResponse response = await _stockService.CreateSellOrder(request);

      Assert.True(response.SellOrderId != Guid.Empty);
    }

    #endregion

    #region GetAllBuyOrders

    [Fact]
    public async void GetAllBuyOrders_DefaultEmptyList()
    {
      List<BuyOrderResponse> listOfBuyOrderResponse = await _stockService.GetBuyOrders();

      Assert.Empty(listOfBuyOrderResponse);
    }

    [Fact]
    public async void GetAllBuyOrders_CorrectWorkOfMethod()
    {
      BuyOrderRequest buyOrderRequest1 = new()
      {
        DateAndTimeOfOrder = DateTime.Parse("2005-04-25"),
        Price = 1488,
        Quantity = 54,
        StockName = "Apple",
        StockSymbol = "AAPL"
      };

      BuyOrderRequest buyOrderRequest2 = new()
      {
        DateAndTimeOfOrder = DateTime.Parse("2005-04-25"),
        Price = 1567,
        Quantity = 15,
        StockName = "Apple",
        StockSymbol = "AAPL"
      };

      BuyOrderRequest buyOrderRequest3 = new()
      {
        DateAndTimeOfOrder = DateTime.Parse("2005-04-25"),
        Price = 4124,
        Quantity = 1412,
        StockName = "Apple",
        StockSymbol = "APPL"
      };

      List<BuyOrderRequest> buyOrderRequests = new List<BuyOrderRequest>
      {
        buyOrderRequest1,
        buyOrderRequest2,
        buyOrderRequest3
      };


      List<BuyOrderResponse> listOfBuyOrderResponsesFromAdd = new List<BuyOrderResponse>();

      foreach (var buyOrderRequest in buyOrderRequests)
      {
        listOfBuyOrderResponsesFromAdd.Add(await _stockService.CreateBuyOrder(buyOrderRequest));
      }

      var listOfBuyOrderResponseFromGet = await _stockService.GetBuyOrders();

      foreach (var buyOrderResponse in listOfBuyOrderResponseFromGet)
      {
        Assert.Contains(buyOrderResponse, listOfBuyOrderResponsesFromAdd);
      }
    }

    #endregion

    #region GetSellOrders

    [Fact]
    public async void GetSellOrders_DefaultEmptyList()
    {
      List<SellOrderResponse> listOfSellOrders = await _stockService.GetSellOrders();

      Assert.Empty(listOfSellOrders);
    }

    [Fact]
    public async void GetSellOrders_CorrectWorkOfMethod()
    {
      List<SellOrderRequest> listOfSellOrderRequests = new List<SellOrderRequest>()
      {
        new SellOrderRequest()
        {
          DateAndTimeOfOrder = DateTime.Parse("2005-04-25"),
          Price = 1442,
          Quantity = 312,
          StockName = "Apple",
          StockSymbol = "AAPL"
        },        
        new SellOrderRequest()
        {
          DateAndTimeOfOrder = DateTime.Parse("2005-04-25"),
          Price = 1442,
          Quantity = 312,
          StockName = "Apple",
          StockSymbol = "AAPL"
        },        
        new SellOrderRequest()
        {
          DateAndTimeOfOrder = DateTime.Parse("2005-04-25"),
          Price = 1442,
          Quantity = 312,
          StockName = "Apple",
          StockSymbol = "AAPL"
        },
      };

      List<SellOrderResponse> listOfSellOrderResponsesFromAdd = new List<SellOrderResponse>();

      foreach (var sellOrderRequest in listOfSellOrderRequests)
      {
        listOfSellOrderResponsesFromAdd.Add( await _stockService.CreateSellOrder(sellOrderRequest));
      }

      List<SellOrderResponse> listOfSellOrderResponseFromGet = await _stockService.GetSellOrders();

      foreach (var sellOrderResponse in listOfSellOrderResponseFromGet)
      {
        Assert.Contains(sellOrderResponse, listOfSellOrderResponsesFromAdd);
      }

    }
    #endregion
  }
}