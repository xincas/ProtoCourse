using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProtoCourse.Data.Migrations
{
    /// <inheritdoc />
    public partial class NullableStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ff68816-30d3-4f02-ad59-b118ddf4acfd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca93c427-b71f-473c-9d70-386d289ba272");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53cf23be-b3b5-478c-ab64-fa95c36fec69", null, "Administrator", "ADMINISTRATOR" },
                    { "94d58938-b660-417a-b31c-1a11d04a4079", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53cf23be-b3b5-478c-ab64-fa95c36fec69");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94d58938-b660-417a-b31c-1a11d04a4079");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ff68816-30d3-4f02-ad59-b118ddf4acfd", null, "Administrator", "ADMINISTRATOR" },
                    { "ca93c427-b71f-473c-9d70-386d289ba272", null, "User", "USER" }
                });
        }
    }
}
