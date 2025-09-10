using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Platform.Migrations
{
    /// <inheritdoc />
    public partial class refactor_fk_item_oncontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityPrintRequests_Items_Id",
                table: "CommunityPrintRequests");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityPrintRequests_ItemId",
                table: "CommunityPrintRequests",
                column: "ItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityPrintRequests_Items_ItemId",
                table: "CommunityPrintRequests",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityPrintRequests_Items_ItemId",
                table: "CommunityPrintRequests");

            migrationBuilder.DropIndex(
                name: "IX_CommunityPrintRequests_ItemId",
                table: "CommunityPrintRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityPrintRequests_Items_Id",
                table: "CommunityPrintRequests",
                column: "Id",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
