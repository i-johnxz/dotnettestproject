using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IFrameworkDemo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "msgs_Commands",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CorrelationId = table.Column<string>(maxLength: 200, nullable: true),
                    MessageBody = table.Column<string>(type: "ntext", nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Topic = table.Column<string>(maxLength: 200, nullable: true),
                    SagaInfo_SagaId = table.Column<string>(nullable: true),
                    SagaInfo_ReplyEndPoint = table.Column<string>(nullable: true),
                    IP = table.Column<string>(nullable: true),
                    Producer = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Result = table.Column<string>(nullable: true),
                    ResultType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_msgs_Commands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "msgs_Events",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CorrelationId = table.Column<string>(maxLength: 200, nullable: true),
                    MessageBody = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Topic = table.Column<string>(maxLength: 200, nullable: true),
                    SagaInfo_SagaId = table.Column<string>(nullable: true),
                    SagaInfo_ReplyEndPoint = table.Column<string>(nullable: true),
                    IP = table.Column<string>(nullable: true),
                    Producer = table.Column<string>(nullable: true),
                    AggregateRootId = table.Column<string>(maxLength: 200, nullable: true),
                    AggregateRootType = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_msgs_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "msgs_HandledEvents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    SubscriptionName = table.Column<string>(maxLength: 322, nullable: false),
                    HandledTime = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Error = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_msgs_HandledEvents", x => new { x.Id, x.SubscriptionName });
                });

            migrationBuilder.CreateTable(
                name: "msgs_UnPublishedEvents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ReplyToEndPoint = table.Column<string>(nullable: true),
                    SagaInfo_SagaId = table.Column<string>(nullable: true),
                    SagaInfo_ReplyEndPoint = table.Column<string>(nullable: true),
                    CorrelationId = table.Column<string>(nullable: true),
                    MessageBody = table.Column<string>(type: "ntext", nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Topic = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(nullable: true),
                    Producer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_msgs_UnPublishedEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "msgs_UnSentCommands",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ReplyToEndPoint = table.Column<string>(nullable: true),
                    SagaInfo_SagaId = table.Column<string>(nullable: true),
                    SagaInfo_ReplyEndPoint = table.Column<string>(nullable: true),
                    CorrelationId = table.Column<string>(nullable: true),
                    MessageBody = table.Column<string>(type: "ntext", nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Topic = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(nullable: true),
                    Producer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_msgs_UnSentCommands", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_msgs_Events_AggregateRootId",
                table: "msgs_Events",
                column: "AggregateRootId");

            migrationBuilder.CreateIndex(
                name: "IX_msgs_Events_CorrelationId",
                table: "msgs_Events",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_msgs_Events_Name",
                table: "msgs_Events",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "msgs_Commands");

            migrationBuilder.DropTable(
                name: "msgs_Events");

            migrationBuilder.DropTable(
                name: "msgs_HandledEvents");

            migrationBuilder.DropTable(
                name: "msgs_UnPublishedEvents");

            migrationBuilder.DropTable(
                name: "msgs_UnSentCommands");
        }
    }
}
