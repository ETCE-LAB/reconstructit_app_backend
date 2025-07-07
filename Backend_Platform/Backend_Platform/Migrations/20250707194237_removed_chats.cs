using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Platform.Migrations
{
    /// <inheritdoc />
    public partial class removed_chats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Chats_ChatId",
                table: "Participants");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "Participants",
                newName: "PrintContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_ChatId",
                table: "Participants",
                newName: "IX_Participants_PrintContractId");

            migrationBuilder.AddColumn<int>(
                name: "PrintMaterial",
                table: "CommunityPrintRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrintContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractStatus = table.Column<int>(type: "int", nullable: false),
                    ShippingStatus = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommunityPrintRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrintContracts_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrintContracts_CommunityPrintRequests_CommunityPrintRequestId",
                        column: x => x.CommunityPrintRequestId,
                        principalTable: "CommunityPrintRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentAttributes_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrintContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PrintContracts_PrintContractId",
                        column: x => x.PrintContractId,
                        principalTable: "PrintContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentValues_PaymentAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "PaymentAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentValues_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAttributes_PaymentMethodId",
                table: "PaymentAttributes",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PrintContractId",
                table: "Payments",
                column: "PrintContractId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentValues_AttributeId",
                table: "PaymentValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentValues_PaymentId",
                table: "PaymentValues",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PrintContracts_AddressId",
                table: "PrintContracts",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PrintContracts_CommunityPrintRequestId",
                table: "PrintContracts",
                column: "CommunityPrintRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_PrintContracts_PrintContractId",
                table: "Participants",
                column: "PrintContractId",
                principalTable: "PrintContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_PrintContracts_PrintContractId",
                table: "Participants");

            migrationBuilder.DropTable(
                name: "PaymentValues");

            migrationBuilder.DropTable(
                name: "PaymentAttributes");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "PrintContracts");

            migrationBuilder.DropColumn(
                name: "PrintMaterial",
                table: "CommunityPrintRequests");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "PrintContractId",
                table: "Participants",
                newName: "ChatId");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_PrintContractId",
                table: "Participants",
                newName: "IX_Participants_ChatId");

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommunityPrintRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chats_CommunityPrintRequests_CommunityPrintRequestId",
                        column: x => x.CommunityPrintRequestId,
                        principalTable: "CommunityPrintRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_AddressId",
                table: "Chats",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_CommunityPrintRequestId",
                table: "Chats",
                column: "CommunityPrintRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ParticipantId",
                table: "Messages",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Chats_ChatId",
                table: "Participants",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
