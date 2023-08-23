using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ProjectHelpers;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
  public class StockService : IStockService
  {
    private List<BuyOrder> _buyOrders;
    private List<SellOrder> _sellOrders;

    public StockService()
    {
        _buyOrders = new List<BuyOrder>();
        _sellOrders = new List<SellOrder>();
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

      _buyOrders.Add(buyOrder);

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

      _sellOrders.Add(sellOrder);

      return sellOrder.ToSellOrderResponse();
    }

    public async Task<List<BuyOrderResponse>> GetBuyOrders()
    {
      if(_buyOrders.Count == 0)
        return new List<BuyOrderResponse>();

      List<BuyOrderResponse> list = new List<BuyOrderResponse>();


      foreach (var buyOrder in _buyOrders)
      {
        list.Add(buyOrder.ToBuyOrderResponse());
      }

      return list;
    }

    public async Task<List<SellOrderResponse>> GetSellOrders()
    {
      if(_sellOrders.Count == 0)
        return new List<SellOrderResponse>();

      List<SellOrderResponse> list = new List<SellOrderResponse>();

      foreach (var sellOrder in _sellOrders)
      {
        list.Add(sellOrder.ToSellOrderResponse());
      }

      return list;
    }
  }
}
