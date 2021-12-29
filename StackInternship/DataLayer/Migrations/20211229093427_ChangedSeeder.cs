using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ChangedSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 1,
                column: "Department",
                value: 53);

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 2,
                column: "Department",
                value: 49);

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "AuthorId", "Content", "DateOfPublishing", "Department", "DownvoteCount", "RoleOfAuthor", "UpvoteCount" },
                values: new object[,]
                {
                    { 3, 2, "Zašto mi se ne ispiše ništa kad stavim where?", new DateTime(2021, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, 0, 0, 0 },
                    { 4, 3, "Upute za LINQ!", new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 49, 0, 0, 0 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsTrustedUser", "Password" },
                values: new object[] { true, "12345" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "UserName" },
                values: new object[] { "54321", "lovre" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "qweqw");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "qwqwq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Password", "UserName" },
                values: new object[] { "asdas", "pzelic" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AuthorId", "CommentId", "Content", "DateOfPublishing", "DownvoteCount", "ResourceId", "UpvoteCount" },
                values: new object[,]
                {
                    { 1, 1, null, "ok", new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, 0 },
                    { 2, 1, null, "Može", new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, 0 }
                });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 1,
                column: "Department",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 2,
                column: "Department",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsTrustedUser", "Password" },
                values: new object[] { false, "1234" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "UserName" },
                values: new object[] { "4321", "lovCik" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "qwe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "ewq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Password", "UserName" },
                values: new object[] { "1950", "pzelich" });
        }
    }
}
