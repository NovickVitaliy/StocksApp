using Microsoft.EntityFrameworkCore;
using Services;
using StockAppWithxUnit.Entities;
using StockAppWithxUnit.Models;
using StockAppWithxUnit.RepositoriesContracts;
using StockAppWithxUnit.RepositoriesImplementations;
using StockAppWithxUnit.ServicesContracts;

namespace StockAppWithxUnit
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);
      builder.Services.AddControllersWithViews();
      builder.Services.AddHttpClient();
      builder.Services.AddScoped<IStockService, StockService>();
      builder.Services.AddScoped<IFinnhubService, FinnhubService>();
      builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();
      builder.Services.AddScoped<IStocksRepository, StocksRepository>();
      builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
      builder.Services.AddDbContext<StockMarketDbContext>(options =>
      {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
      });

      var app = builder.Build();

      Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", "Rotativa");

      app.UseStaticFiles();
      app.UseRouting();
      app.MapControllers();


      app.Run();
    }
  }
}