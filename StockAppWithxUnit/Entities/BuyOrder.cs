using System.ComponentModel.DataAnnotations;
using ServiceContracts.DTO;

namespace Entities
{
  public class BuyOrder
  {
    public Guid? BuyOrderId { get; set; }
    [Required(ErrorMessage = "{0} must be supplied")]
    public string? StockSymbol { get; set; }
    [Required(ErrorMessage = "{0} must be supplied")]
    public string? StockName { get; set; }
    public DateTime? DateAndTimeOfOrder { get; set; }
    [Range(1, 100000, ErrorMessage = "Quantity should be between 1 and 100000")]
    public uint Quantity { get; set; }
    [Range(1, 10000, ErrorMessage = "Price should be between 1 and 10000")]
    public double Price { get; set; }
  }

  public static class BuyOrderExtensions
  {
    public static BuyOrder ToBuyOrder(this BuyOrderRequest buyOrderRequest)
    {
      return new BuyOrder()
      {
        StockName = buyOrderRequest.StockName,
        StockSymbol = buyOrderRequest.StockSymbol,
        DateAndTimeOfOrder = buyOrderRequest?.DateAndTimeOfOrder,
        Price = buyOrderRequest.Price,
        Quantity = buyOrderRequest.Quantity,
      };
    }
    public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
    {
      return new BuyOrderResponse()
      {
        BuyOrderID = buyOrder.BuyOrderId,
        DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
        Price = buyOrder.Price,
        Quantity = buyOrder.Quantity,
        StockName = buyOrder.StockName,
        StockSymbol = buyOrder.StockSymbol,
        TradeAmount = buyOrder.Price * buyOrder.Quantity
      };
    } 
  }
}