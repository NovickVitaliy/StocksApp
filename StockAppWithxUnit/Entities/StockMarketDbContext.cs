using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO;

namespace StockAppWithxUnit.Entities
{
  public class StockMarketDbContext : DbContext
  {
    public DbSet<BuyOrder> BuyOrders { get; set; }
    public DbSet<SellOrder> SellOrders { get; set; }

    public StockMarketDbContext(DbContextOptions<StockMarketDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<BuyOrder>().ToTable("BuyOrders");
      modelBuilder.Entity<SellOrder>().ToTable("SellOrders");
    }

    public List<BuyOrder> sp_GetBuyOrders()
    {
      return BuyOrders.FromSqlRaw("EXECUTE [dbo].[GetBuyOrders]").ToList();
    }

    public List<SellOrder> sp_GetSellOrders()
    {
      return SellOrders.FromSqlRaw("EXECUTE [dbo].[GetSellOrders]").ToList();
    }
  }
}
