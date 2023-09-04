using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoredProcTest.Migrations
{
    /// <inheritdoc />
    public partial class DataAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "ClassId", "ClassName" },
                values: new object[,]
                {
                    { 1, "English" },
                    { 2, "Math" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentId", "Age", "ClassId", "Name" },
                values: new object[,]
                {
                    { new Guid("0fdb95e3-af14-45fe-bba9-fcb860708bc4"), 25, 2, "Jane Doe" },
                    { new Guid("3763ec80-2d36-47c9-acf7-4a053fb18a1b"), 30, 1, "John Doe" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: new Guid("0fdb95e3-af14-45fe-bba9-fcb860708bc4"));

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: new Guid("3763ec80-2d36-47c9-acf7-4a053fb18a1b"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 2);
        }
    }
}
