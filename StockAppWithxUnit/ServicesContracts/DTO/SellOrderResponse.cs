using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
  public class SellOrderResponse
  {
    public Guid? SellOrderId { get; set; }
    public string? StockSymbol { get; set; }
    public string? StockName { get;set; }
    public DateTime? DateAndTimeOfOrder { get; set; }
    public uint Quantity { get; set; }
    public double Price { get; set; }
    public double TradeAmount { get; set; }

    public override bool Equals(object? obj)
    {
      if (obj == null) return false;
      if (obj.GetType() != typeof(SellOrderResponse)) return false;

      SellOrderResponse sellOrder = (SellOrderResponse)obj;

      return SellOrderId == sellOrder.SellOrderId &&
             StockSymbol == sellOrder.StockSymbol &&
             StockName == sellOrder.StockName &&
             DateAndTimeOfOrder == sellOrder.DateAndTimeOfOrder &&
             Quantity == sellOrder.Quantity &&
             Price == sellOrder.Price &&
             TradeAmount == sellOrder.Price * sellOrder.Quantity;
    }
  }
}

