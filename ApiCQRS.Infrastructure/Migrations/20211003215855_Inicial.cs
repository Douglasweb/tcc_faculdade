using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiCQRS.Infrastructure.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    UserCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserStatus = table.Column<bool>(type: "bit", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanBeUpdated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "CanBeUpdated", "UserCreatedAt", "UserEmail", "UserName", "UserPassword", "UserStatus", "UserUpdatedAt" },
                values: new object[] { new Guid("310d95d4-9f90-4db7-ac52-fa85a1d08622"), true, new DateTime(2021, 10, 3, 18, 58, 54, 734, DateTimeKind.Local).AddTicks(1398), "webdouglasti@gmail.com", "Douglas Eduardo", "0xB123E9E19D217169B981A61188920F9D28638709A5132201684D792B9264271B7F09157ED4321B1C097F7A4ABECFC0977D40A7EE599C845883BD1074CA23C4AF", true, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
