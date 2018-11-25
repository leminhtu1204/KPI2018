using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LunchManagement.Migrations
{
    public partial class updateCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LunchOrders_AspNetUsers_UserId",
                table: "LunchOrders");

            migrationBuilder.DropIndex(
                name: "IX_LunchOrders_UserId",
                table: "LunchOrders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LunchOrders");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "LunchOrders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdentityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_AspNetUsers_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LunchOrders_CustomerId",
                table: "LunchOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IdentityId",
                table: "Customer",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_LunchOrders_Customer_CustomerId",
                table: "LunchOrders",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LunchOrders_Customer_CustomerId",
                table: "LunchOrders");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_LunchOrders_CustomerId",
                table: "LunchOrders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "LunchOrders");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LunchOrders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LunchOrders_UserId",
                table: "LunchOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LunchOrders_AspNetUsers_UserId",
                table: "LunchOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
