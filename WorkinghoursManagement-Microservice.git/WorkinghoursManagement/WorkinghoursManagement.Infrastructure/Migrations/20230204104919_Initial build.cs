using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WorkinghoursManagement.Infrastructure.Migrations
{
    public partial class Initialbuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "WorkingHoursByUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_{nameof(WorkingHoursByUser)}", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingHoursByUser_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreationDatetime", "IsActive" },
                values: new object[,]
                {
                    { new Guid("5997c8dc-b95e-45dc-a7b6-622f5d087da7"), new DateTime(2023, 2, 4, 11, 49, 18, 696, DateTimeKind.Local).AddTicks(3458), true },
                    { new Guid("42030bd2-0ec4-445a-84fd-71bee25e3405"), new DateTime(2023, 2, 4, 11, 49, 18, 698, DateTimeKind.Local).AddTicks(5007), true },
                    { new Guid("37b89795-4297-4678-8193-c98470a721c2"), new DateTime(2023, 2, 4, 11, 49, 18, 698, DateTimeKind.Local).AddTicks(5023), true },
                    { new Guid("bf935030-aa9b-463f-8637-357ac08e8d1c"), new DateTime(2023, 2, 4, 11, 49, 18, 698, DateTimeKind.Local).AddTicks(5026), true }
                });

            migrationBuilder.InsertData(
                table: "WorkingHoursByUsers",
                columns: new[] { "Id", "EndDateTime", "StartDateTime", "UserId" },
                values: new object[,]
                {
                    { new Guid("18be82f0-40d9-4678-9592-96bc64fe0cda"), new DateTime(2023, 2, 4, 19, 49, 18, 699, DateTimeKind.Local).AddTicks(2144), new DateTime(2023, 2, 4, 11, 49, 18, 699, DateTimeKind.Local).AddTicks(1970), new Guid("5997c8dc-b95e-45dc-a7b6-622f5d087da7") },
                    { new Guid("7f3ed7ad-8271-475d-a457-997d00c47b4f"), new DateTime(2023, 2, 4, 19, 49, 18, 699, DateTimeKind.Local).AddTicks(2296), new DateTime(2023, 2, 4, 11, 49, 18, 699, DateTimeKind.Local).AddTicks(2293), new Guid("42030bd2-0ec4-445a-84fd-71bee25e3405") },
                    { new Guid("ae16eef2-8cfc-4253-a966-15fa7567c698"), new DateTime(2023, 2, 4, 19, 49, 18, 699, DateTimeKind.Local).AddTicks(2309), new DateTime(2023, 2, 4, 11, 49, 18, 699, DateTimeKind.Local).AddTicks(2308), new Guid("37b89795-4297-4678-8193-c98470a721c2") },
                    { new Guid("6c8e3d98-45b6-48a0-9c2b-36590d300df7"), new DateTime(2023, 2, 4, 19, 49, 18, 699, DateTimeKind.Local).AddTicks(2313), new DateTime(2023, 2, 4, 11, 49, 18, 699, DateTimeKind.Local).AddTicks(2312), new Guid("bf935030-aa9b-463f-8637-357ac08e8d1c") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHoursByUsers_UserId",
                table: "WorkingHoursByUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingHoursByUsers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}