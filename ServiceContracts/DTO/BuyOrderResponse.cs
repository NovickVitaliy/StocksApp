using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
  public class BuyOrderResponse
  {
    public Guid? BuyOrderID { get; set; }
    public string? StockSymbol { get; set; }
    public string? StockName { get;set; }
    public DateTime? DateAndTimeOfOrder { get; set; }
    public uint Quantity { get; set; }
    public double Price { get; set; }
    public double TradeAmount { get; set; }

    public override bool Equals(object? obj)
    {
      if (obj == null) return false;
      if(obj.GetType() != typeof(BuyOrderResponse)) return false;

      BuyOrderResponse buyOrder = (BuyOrderResponse)obj;

      return BuyOrderID == buyOrder.BuyOrderID &&
             StockSymbol == buyOrder.StockSymbol &&
             StockName == buyOrder.StockName &&
             DateAndTimeOfOrder == buyOrder.DateAndTimeOfOrder &&
             Quantity == buyOrder.Quantity &&
             Price == buyOrder.Price &&
             TradeAmount == buyOrder.Price * buyOrder.Quantity;
    }
  }
}
