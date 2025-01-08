using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionsAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerFullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerFullNameHebrew = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerDateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
