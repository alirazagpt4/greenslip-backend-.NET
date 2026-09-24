using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace greenslip_backend.Migrations
{
    /// <inheritdoc />
    public partial class LinkInvoiceToStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Invoices_StoreId",
                table: "Invoices",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Stores_StoreId",
                table: "Invoices",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Stores_StoreId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_StoreId",
                table: "Invoices");
        }
    }
}
