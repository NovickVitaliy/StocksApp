using Entities;
using Microsoft.EntityFrameworkCore;
using StockAppWithxUnit.Entities;
using StockAppWithxUnit.RepositoriesContracts;

namespace StockAppWithxUnit.RepositoriesImplementations
{
  public class StocksRepository : IStocksRepository
  {
    private readonly StockMarketDbContext _db;

    public StocksRepository(StockMarketDbContext db)
    {
      _db = db;
    }

    public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
    {
      _db.BuyOrders.Add(buyOrder);
      await _db.SaveChangesAsync();
      return buyOrder;
    }

    public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
    {
      _db.SellOrders.Add(sellOrder);
      await _db.SaveChangesAsync();
      return sellOrder;
    }

    public async Task<List<BuyOrder>> GetBuyOrders()
    {
      return _db.sp_GetBuyOrders();
    }

    public async Task<List<SellOrder>> GetSellOrders()
    {
      return _db.sp_GetSellOrders();
    }
  }
}
