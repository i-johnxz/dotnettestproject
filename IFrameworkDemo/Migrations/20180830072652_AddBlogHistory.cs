using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IFrameworkDemo.Migrations
{
    public partial class AddBlogHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogHistorys",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BlogTitle = table.Column<string>(nullable: true),
                    BlogContent = table.Column<string>(nullable: true),
                    BlogCreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogHistorys", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogHistorys");
        }
    }
}
