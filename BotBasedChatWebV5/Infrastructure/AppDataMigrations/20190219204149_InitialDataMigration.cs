using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BotBasedChatWebV5.Infrastructure.AppDataMigrations
{
    public partial class InitialDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "message_seq",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(nullable: false),
                    Room = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    ProfileUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropSequence(
                name: "message_seq");
        }
    }
}
