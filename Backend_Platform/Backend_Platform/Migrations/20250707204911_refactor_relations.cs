using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Platform.Migrations
{
    /// <inheritdoc />
    public partial class refactor_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_PrintContracts_PrintContractId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PrintContracts_PrintContractId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentValues_PaymentAttributes_AttributeId",
                table: "PaymentValues");

            migrationBuilder.DropForeignKey(
                name: "FK_PrintContracts_CommunityPrintRequests_CommunityPrintRequestId",
                table: "PrintContracts");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PrintContractId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "AttributeId",
                table: "PaymentValues",
                newName: "PaymentAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentValues_AttributeId",
                table: "PaymentValues",
                newName: "IX_PaymentValues_PaymentAttributeId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CommunityPrintRequestId",
                table: "PrintContracts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "PrintContracts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentId",
                table: "PaymentValues",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentMethodId",
                table: "PaymentAttributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PrintContractId",
                table: "Payments",
                column: "PrintContractId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_PrintContracts_PrintContractId",
                table: "Participants",
                column: "PrintContractId",
                principalTable: "PrintContracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PrintContracts_PrintContractId",
                table: "Payments",
                column: "PrintContractId",
                principalTable: "PrintContracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentValues_PaymentAttributes_PaymentAttributeId",
                table: "PaymentValues",
                column: "PaymentAttributeId",
                principalTable: "PaymentAttributes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrintContracts_CommunityPrintRequests_CommunityPrintRequestId",
                table: "PrintContracts",
                column: "CommunityPrintRequestId",
                principalTable: "CommunityPrintRequests",
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
                name: "FK_Payments_PaymentMethods_PaymentMethodId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PrintContracts_PrintContractId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentValues_PaymentAttributes_PaymentAttributeId",
                table: "PaymentValues");

            migrationBuilder.DropForeignKey(
                name: "FK_PrintContracts_CommunityPrintRequests_CommunityPrintRequestId",
                table: "PrintContracts");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PrintContractId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "PrintContracts");

            migrationBuilder.RenameColumn(
                name: "PaymentAttributeId",
                table: "PaymentValues",
                newName: "AttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentValues_PaymentAttributeId",
                table: "PaymentValues",
                newName: "IX_PaymentValues_AttributeId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CommunityPrintRequestId",
                table: "PrintContracts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentId",
                table: "PaymentValues",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentMethodId",
                table: "PaymentAttributes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PrintContractId",
                table: "Payments",
                column: "PrintContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_PrintContracts_PrintContractId",
                table: "Participants",
                column: "PrintContractId",
                principalTable: "PrintContracts",
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
                name: "FK_Payments_PrintContracts_PrintContractId",
                table: "Payments",
                column: "PrintContractId",
                principalTable: "PrintContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentValues_PaymentAttributes_AttributeId",
                table: "PaymentValues",
                column: "AttributeId",
                principalTable: "PaymentAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrintContracts_CommunityPrintRequests_CommunityPrintRequestId",
                table: "PrintContracts",
                column: "CommunityPrintRequestId",
                principalTable: "CommunityPrintRequests",
                principalColumn: "Id");
        }
    }
}
