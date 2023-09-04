using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoredProcTest.Migrations
{
    /// <inheritdoc />
    public partial class DBContextAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: new Guid("949ec777-893c-43be-a53b-33b48f69efed"));

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: new Guid("feb5c3af-c711-4965-9087-f7ac560ab5dd"));

            migrationBuilder.CreateTable(
                name: "First",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Second",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentId", "Age", "ClassId", "Name" },
                values: new object[,]
                {
                    { new Guid("3a7484f9-b5ff-4279-afe4-f524cbf567de"), 25, 2, "Jane Doe" },
                    { new Guid("dd656c83-faec-4e79-9c2c-f46ea93b031f"), 30, 1, "John Doe" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "First");

            migrationBuilder.DropTable(
                name: "Second");

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: new Guid("3a7484f9-b5ff-4279-afe4-f524cbf567de"));

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: new Guid("dd656c83-faec-4e79-9c2c-f46ea93b031f"));

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentId", "Age", "ClassId", "Name" },
                values: new object[,]
                {
                    { new Guid("949ec777-893c-43be-a53b-33b48f69efed"), 25, 2, "Jane Doe" },
                    { new Guid("feb5c3af-c711-4965-9087-f7ac560ab5dd"), 30, 1, "John Doe" }
                });
        }
    }
}
