using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts.DTO;

namespace Entities
{
  public class SellOrder
  {
    public Guid? SellOrderId { get; set; }
    [Required(ErrorMessage = "{0} must be supplied")]
    public string? StockSymbol { get; set; }
    [Required(ErrorMessage = "{0} must be supplied")]
    public string? StockName { get;set; }
    public DateTime? DateAndTimeOfOrder { get; set; }
    [Range(1, 100000, ErrorMessage = "Quantity should be between 1 and 100000")]
    public uint Quantity { get; set; }
    [Range(1, 10000, ErrorMessage = "Price should be between 1 and 10000")]
    public double Price { get; set; }
  }

  public static class SellOrderExtensions
  {
    public static SellOrder ToSellOrder(this SellOrderRequest sellOrderRequest)
    {
      return new SellOrder()
      {
        StockName = sellOrderRequest.StockName,
        Price = sellOrderRequest.Price,
        StockSymbol = sellOrderRequest.StockSymbol,
        Quantity = sellOrderRequest.Quantity,
        DateAndTimeOfOrder = sellOrderRequest.DateAndTimeOfOrder,
      };
    }
    public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
    {
      return new SellOrderResponse()
      {
        DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
        Price = sellOrder.Price,
        Quantity = sellOrder.Quantity,
        SellOrderId = sellOrder.SellOrderId,
        StockName = sellOrder.StockName,
        StockSymbol = sellOrder.StockSymbol,
        TradeAmount = sellOrder.Price * sellOrder.Quantity
      };
    }
  }
}
