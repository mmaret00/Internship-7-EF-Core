using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class RestorationAfterDeletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    IsTrustedUser = table.Column<bool>(type: "bit", nullable: false),
                    DeactivatedUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PermanentDeactivation = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    UpvoteCount = table.Column<int>(type: "int", nullable: false),
                    DownvoteCount = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    TypeOfEntry = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id");
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
                    Voted = table.Column<bool>(type: "bit", nullable: false)
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
                table: "Users",
                columns: new[] { "Id", "DeactivatedUntil", "IsTrustedUser", "Password", "PermanentDeactivation", "ReputationPoints", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, null, false, "12345", false, 10, 0, "MateaB" },
                    { 2, null, true, "54321", false, 444444, 1, "lovre" },
                    { 3, null, false, "qweqw", false, 1, 0, "bartol_deak" },
                    { 4, null, false, "qwqwq", false, 990, 0, "mmaretic" },
                    { 5, null, false, "password", false, 1, 0, "Anamarija" },
                    { 6, null, false, "sifra", false, 1, 0, "Boze Topic" },
                    { 7, null, true, "asdas", false, 200000, 1, "petra123" }
                });

            migrationBuilder.InsertData(
                table: "Entries",
                columns: new[] { "Id", "AuthorId", "CommentCount", "Content", "Department", "DownvoteCount", "ParentId", "PublishedAt", "TypeOfEntry", "UpvoteCount", "ViewCount" },
                values: new object[,]
                {
                    { 1, 1, 1, "Integer ac neque. Duis bibendum. Morbi non quam nec dui luctus rutrum. Nulla tellus. In sagittis dui vel nisl.", 53, 0, 0, new DateTime(2021, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 3, 2, 0, "Vivamus vestibulum sagittis sapien. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Etiam vel augue. Vestibulum rutrum rutrum neque.", 50, 0, 0, new DateTime(2021, 12, 25, 14, 30, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 2, 3, 0, "Integer pede justo, lacinia eget, tincidunt eget, tempus vel, pede. Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem. Fusce consequat. Nulla nisl. Nunc nisl.", 49, 0, 0, new DateTime(2021, 12, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 4, 3, 0, "Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus. Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis. Integer aliquet, massa id lobortis convallis, tortor risus dapibus augue, vel accumsan tellus nisi eu orci. Mauris lacinia sapien quis libero. Nullam sit amet turpis elementum ligula vehicula consequat.", 49, 0, 0, new DateTime(2021, 12, 1, 0, 30, 0, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 5, 3, 0, "Aenean auctor gravida sem. Praesent id massa id nisl venenatis lacinia. Aenean sit amet justo. Morbi ut odio. Cras mi pede, malesuada in, imperdiet et, commodo vulputate, justo. In blandit ultrices enim.", 53, 0, 0, new DateTime(2021, 12, 1, 23, 59, 59, 0, DateTimeKind.Unspecified), 0, 0, 0 },
                    { 6, 3, 1, "Quisque erat eros, viverra eget.", 53, 0, 1, new DateTime(2022, 1, 1, 12, 13, 14, 0, DateTimeKind.Unspecified), 1, 0, 0 },
                    { 7, 4, 0, "Maecenas ut massa quis augue luctus tincidunt.", 53, 0, 6, new DateTime(2022, 1, 3, 12, 13, 14, 0, DateTimeKind.Unspecified), 2, 0, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entries_AuthorId",
                table: "Entries",
                column: "AuthorId");

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
