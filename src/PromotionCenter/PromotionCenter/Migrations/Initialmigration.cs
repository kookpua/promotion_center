using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionCenter.Migrations
{
    public partial class Initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
             name: "PromotionProduct",
             columns: table => new
             {
                 Id = table.Column<int>(nullable: false),
                 PromotionId = table.Column<int>(nullable: false),
                 ProductId = table.Column<int>(nullable: false),
                 Price = table.Column<decimal>(nullable: false),
                 StockQuantity = table.Column<int>(nullable: true),
                 Deleted = table.Column<bool>(nullable: false)
             });

            // Additional code not shown
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionProduct");
            // Additional code not shown
        }
    }
}
