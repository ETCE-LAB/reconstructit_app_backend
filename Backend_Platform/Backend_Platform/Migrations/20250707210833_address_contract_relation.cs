using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Platform.Migrations
{
    /// <inheritdoc />
    public partial class address_contract_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrintContracts_Addresses_AddressId",
                table: "PrintContracts");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "PrintContracts",
                newName: "RevealedAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_PrintContracts_AddressId",
                table: "PrintContracts",
                newName: "IX_PrintContracts_RevealedAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrintContracts_Addresses_RevealedAddressId",
                table: "PrintContracts",
                column: "RevealedAddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrintContracts_Addresses_RevealedAddressId",
                table: "PrintContracts");

            migrationBuilder.RenameColumn(
                name: "RevealedAddressId",
                table: "PrintContracts",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_PrintContracts_RevealedAddressId",
                table: "PrintContracts",
                newName: "IX_PrintContracts_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrintContracts_Addresses_AddressId",
                table: "PrintContracts",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
