using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoredProcTest.Migrations
{
    /// <inheritdoc />
    public partial class AddIgnoredDBSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: new Guid("0fdb95e3-af14-45fe-bba9-fcb860708bc4"));

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: new Guid("3763ec80-2d36-47c9-acf7-4a053fb18a1b"));

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentId", "Age", "ClassId", "Name" },
                values: new object[,]
                {
                    { new Guid("949ec777-893c-43be-a53b-33b48f69efed"), 25, 2, "Jane Doe" },
                    { new Guid("feb5c3af-c711-4965-9087-f7ac560ab5dd"), 30, 1, "John Doe" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: new Guid("949ec777-893c-43be-a53b-33b48f69efed"));

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: new Guid("feb5c3af-c711-4965-9087-f7ac560ab5dd"));

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentId", "Age", "ClassId", "Name" },
                values: new object[,]
                {
                    { new Guid("0fdb95e3-af14-45fe-bba9-fcb860708bc4"), 25, 2, "Jane Doe" },
                    { new Guid("3763ec80-2d36-47c9-acf7-4a053fb18a1b"), 30, 1, "John Doe" }
                });
        }
    }
}
