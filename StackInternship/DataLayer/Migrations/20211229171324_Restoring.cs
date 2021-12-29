using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Restoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    UpvoteCount = table.Column<int>(type: "int", nullable: false),
                    DownvoteCount = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    DateOfPublishing = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReputationPoints = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsTrustedUser = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EntryId = table.Column<int>(type: "int", nullable: false),
                    Viewed = table.Column<bool>(type: "bit", nullable: false),
                    Upvoted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEntries_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserEntries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Entries",
                columns: new[] { "Id", "AuthorId", "Content", "DateOfPublishing", "Department", "Discriminator", "DownvoteCount", "ParentId", "UpvoteCount", "ViewCount" },
                values: new object[] { 100, 2, "ok", new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Comment", 0, 1, 0, 0 });

            migrationBuilder.InsertData(
                table: "Entries",
                columns: new[] { "Id", "AuthorId", "Content", "DateOfPublishing", "Department", "Discriminator", "DownvoteCount", "UpvoteCount", "ViewCount" },
                values: new object[,]
                {
                    { 1, 1, "Prva obavijest", new DateTime(2021, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), 53, "Entry", 0, 0, 0 },
                    { 2, 3, "Kad je iduće predavanje?", new DateTime(2021, 12, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), 49, "Entry", 0, 0, 0 },
                    { 3, 2, "Zašto mi se ne ispiše ništa kad stavim where?", new DateTime(2021, 12, 25, 14, 30, 0, 0, DateTimeKind.Unspecified), 50, "Entry", 0, 0, 0 },
                    { 4, 3, "Upute za LINQ!", new DateTime(2021, 12, 1, 0, 30, 0, 0, DateTimeKind.Unspecified), 49, "Entry", 0, 0, 0 },
                    { 5, 3, "Uskršnji party je u učionici na Veliki petak u 19h, nemojte kasnit!", new DateTime(2021, 12, 1, 23, 59, 59, 0, DateTimeKind.Unspecified), 53, "Entry", 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsTrustedUser", "Password", "ReputationPoints", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, true, "12345", 10, 0, "MateaB" },
                    { 2, true, "54321", 44444, 1, "lovre" },
                    { 3, false, "qweqw", 1, 0, "bartol_deak" },
                    { 4, true, "qwqwq", 1000, 0, "mmaretic" },
                    { 5, false, "password", 1, 0, "anamarija" },
                    { 6, false, "sifra", 1, 0, "boze topic" },
                    { 7, true, "asdas", 10000, 1, "petra123" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEntries_EntryId",
                table: "UserEntries",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEntries_UserId",
                table: "UserEntries",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEntries");

            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
