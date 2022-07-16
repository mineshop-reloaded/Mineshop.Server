using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddPaymentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    MinecraftPlayer = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    PaymentGateway = table.Column<int>(type: "integer", nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "PaymentProducts",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentIdentifier = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductIdentifier = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProducts", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_PaymentProducts_Payments_PaymentIdentifier",
                        column: x => x.PaymentIdentifier,
                        principalTable: "Payments",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentProducts_Products_ProductIdentifier",
                        column: x => x.ProductIdentifier,
                        principalTable: "Products",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProducts_PaymentIdentifier",
                table: "PaymentProducts",
                column: "PaymentIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProducts_ProductIdentifier",
                table: "PaymentProducts",
                column: "ProductIdentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentProducts");

            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
