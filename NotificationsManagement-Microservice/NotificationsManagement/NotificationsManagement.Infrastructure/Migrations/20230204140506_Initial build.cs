using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotificationsManagement.Infrastructure.Migrations
{
    public partial class Initialbuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationTypeName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationsSent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_{nameof(NotificationSent)}", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationSent_NotificationType",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationSent_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "NotificationTypeName" },
                values: new object[,]
                {
                    { new Guid("2e09803f-2d04-4a1b-8ce2-527958b17e59"), "Congrats" },
                    { new Guid("dce80c72-6f19-49d2-8034-2501dfa08cc7"), "Warning" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreationDatetime", "IsActive" },
                values: new object[,]
                {
                    { new Guid("5997c8dc-b95e-45dc-a7b6-622f5d087da7"), new DateTime(2023, 2, 4, 15, 5, 6, 218, DateTimeKind.Local).AddTicks(7396), true },
                    { new Guid("42030bd2-0ec4-445a-84fd-71bee25e3405"), new DateTime(2023, 2, 4, 15, 5, 6, 221, DateTimeKind.Local).AddTicks(6240), true },
                    { new Guid("37b89795-4297-4678-8193-c98470a721c2"), new DateTime(2023, 2, 4, 15, 5, 6, 221, DateTimeKind.Local).AddTicks(6442), true },
                    { new Guid("bf935030-aa9b-463f-8637-357ac08e8d1c"), new DateTime(2023, 2, 4, 15, 5, 6, 221, DateTimeKind.Local).AddTicks(6448), true }
                });

            migrationBuilder.InsertData(
                table: "NotificationsSent",
                columns: new[] { "Id", "NotificationDatetime", "NotificationMessage", "NotificationTypeId", "UserId" },
                values: new object[,]
                {
                    { new Guid("1c0e12d7-0b39-464f-9c00-a4a29b7948ff"), new DateTime(2023, 2, 4, 15, 5, 6, 222, DateTimeKind.Local).AddTicks(6457), "Congrats on this achievment! Keep up the good work!", new Guid("2e09803f-2d04-4a1b-8ce2-527958b17e59"), new Guid("5997c8dc-b95e-45dc-a7b6-622f5d087da7") },
                    { new Guid("56db6512-3c96-4072-b84c-7735477b1aa7"), new DateTime(2023, 2, 4, 15, 5, 6, 222, DateTimeKind.Local).AddTicks(6707), "Congrats on this achievment! Keep up the good work!", new Guid("2e09803f-2d04-4a1b-8ce2-527958b17e59"), new Guid("42030bd2-0ec4-445a-84fd-71bee25e3405") },
                    { new Guid("04683347-9718-4f82-af5e-8355edc0a2a8"), new DateTime(2023, 2, 4, 15, 5, 6, 222, DateTimeKind.Local).AddTicks(6723), "Congrats on this achievment! Keep up the good work!", new Guid("2e09803f-2d04-4a1b-8ce2-527958b17e59"), new Guid("37b89795-4297-4678-8193-c98470a721c2") },
                    { new Guid("f668b4ce-597c-44d3-8383-fdb60eaf06f3"), new DateTime(2023, 2, 4, 15, 5, 6, 222, DateTimeKind.Local).AddTicks(6729), "Congrats on this achievment! Keep up the good work!", new Guid("2e09803f-2d04-4a1b-8ce2-527958b17e59"), new Guid("bf935030-aa9b-463f-8637-357ac08e8d1c") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationsSent_NotificationTypeId",
                table: "NotificationsSent",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationsSent_UserId",
                table: "NotificationsSent",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationsSent");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
