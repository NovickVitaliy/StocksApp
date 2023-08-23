using ServiceContracts.DTO;

namespace StockAppWithxUnit.Models
{
  public class Orders
  {
    public List<BuyOrderResponse> BuyOrders { get; set; }
    public List<SellOrderResponse> SellOrders { get; set; }
  }
}
