using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockAppWithxUnit.Migrations
{
    /// <inheritdoc />
    public partial class GetBuyOrders_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          string sql = @"
                      CREATE PROCEDURE [dbo].[GetBuyOrders]
                        AS BEGIN
                         SELECT * FROM [dbo].[BuyOrders]
                        END
                      ";

          migrationBuilder.Sql(sql);
    }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          string sql = @"DROP PROCEDURE [dbo].[GetBuyOrders]";
        }
    }
}
