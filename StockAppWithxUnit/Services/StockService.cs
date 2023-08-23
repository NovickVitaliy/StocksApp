using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ProjectHelpers;
using ServiceContracts.DTO;
using StockAppWithxUnit.Entities;
using StockAppWithxUnit.RepositoriesContracts;
using StockAppWithxUnit.ServicesContracts;

namespace Services
{
  public class StockService : IStockService
  {
    private readonly IStocksRepository _stocksRepository;
    public StockService(IStocksRepository stocksRepository)
    {
      _stocksRepository = stocksRepository;
    }

    public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
    {
      if (buyOrderRequest == null) throw new ArgumentNullException(nameof(buyOrderRequest));
      ModelValidator.Validate(buyOrderRequest);

      BuyOrder buyOrder = new BuyOrder()
      {
        BuyOrderId = Guid.NewGuid(),
        DateAndTimeOfOrder = buyOrderRequest.DateAndTimeOfOrder,
        Price = buyOrderRequest.Price,
        Quantity = buyOrderRequest.Quantity,
        StockName = buyOrderRequest.StockName,
        StockSymbol = buyOrderRequest.StockSymbol
      };

      await _stocksRepository.CreateBuyOrder(buyOrder);

      return buyOrder.ToBuyOrderResponse();
    }

    public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
    {
      if (sellOrderRequest == null) throw new ArgumentNullException(nameof(sellOrderRequest));
      ModelValidator.Validate(sellOrderRequest);

      SellOrder sellOrder = new SellOrder()
      {
        SellOrderId = Guid.NewGuid(),
        DateAndTimeOfOrder = sellOrderRequest.DateAndTimeOfOrder,
        Price = sellOrderRequest.Price,
        Quantity = sellOrderRequest.Quantity,
        StockName = sellOrderRequest.StockName,
        StockSymbol = sellOrderRequest.StockSymbol
      };

      await _stocksRepository.CreateSellOrder(sellOrder);

      return sellOrder.ToSellOrderResponse();
    }

    public async Task<List<BuyOrderResponse>> GetBuyOrders()
    {
      return (await _stocksRepository.GetBuyOrders()).Select(buyOrder => buyOrder.ToBuyOrderResponse()).ToList();
    }

    public async Task<List<SellOrderResponse>> GetSellOrders()
    {
      return (await _stocksRepository.GetSellOrders()).Select(sellOrder => sellOrder.ToSellOrderResponse()).ToList();
    }
  }
}
