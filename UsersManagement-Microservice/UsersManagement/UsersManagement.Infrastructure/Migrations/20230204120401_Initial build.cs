using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace UsersManagement.Infrastructure.Migrations
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
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("5997c8dc-b95e-45dc-a7b6-622f5d087da7"), "rony@mail.com", true, "Rony Cuzco", "Password12023" },
                    { new Guid("42030bd2-0ec4-445a-84fd-71bee25e3405"), "julie@mail.com", true, "Julie Zarco", "Password12023" },
                    { new Guid("37b89795-4297-4678-8193-c98470a721c2"), "ariadne@mail.com", true, "Ariadne Cuzco Zarco", "Password12023" },
                    { new Guid("bf935030-aa9b-463f-8637-357ac08e8d1c"), "trufa@mail.com", true, "Truferia Zarco", "Password12023" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}