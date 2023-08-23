using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockAppWithxUnit.Migrations
{
  /// <inheritdoc />
  public partial class GetSellOrders_StoredProcedure : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      string sql = @"
                      CREATE PROCEDURE [dbo].[GetSellOrders]
                        AS BEGIN
                         SELECT * FROM [dbo].[SellOrders]
                        END
                      ";

      migrationBuilder.Sql(sql);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      string sql = @"
                      DROP PROCEDURE [dbo].[GetSellOrders]
                      ";

      migrationBuilder.Sql(sql);
    }
  }
}
