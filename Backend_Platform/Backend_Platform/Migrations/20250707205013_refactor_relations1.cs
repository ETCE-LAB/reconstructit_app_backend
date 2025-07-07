using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Platform.Migrations
{
    /// <inheritdoc />
    public partial class refactor_relations1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_PrintContracts_PrintContractId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentAttributes_PaymentMethods_PaymentMethodId",
                table: "PaymentAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentValues_PaymentAttributes_PaymentAttributeId",
                table: "PaymentValues");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_PrintContracts_PrintContractId",
                table: "Participants",
                column: "PrintContractId",
                principalTable: "PrintContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentAttributes_PaymentMethods_PaymentMethodId",
                table: "PaymentAttributes",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentValues_PaymentAttributes_PaymentAttributeId",
                table: "PaymentValues",
                column: "PaymentAttributeId",
                principalTable: "PaymentAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_PrintContracts_PrintContractId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentAttributes_PaymentMethods_PaymentMethodId",
                table: "PaymentAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentValues_PaymentAttributes_PaymentAttributeId",
                table: "PaymentValues");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_PrintContracts_PrintContractId",
                table: "Participants",
                column: "PrintContractId",
                principalTable: "PrintContracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentAttributes_PaymentMethods_PaymentMethodId",
                table: "PaymentAttributes",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentValues_PaymentAttributes_PaymentAttributeId",
                table: "PaymentValues",
                column: "PaymentAttributeId",
                principalTable: "PaymentAttributes",
                principalColumn: "Id");
        }
    }
}
