using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szoftverfejlesztés_dotnet_hw.Migrations
{
    /// <inheritdoc />
    public partial class extra_seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Creatorname", "Groupname" },
                values: new object[] { 1, "admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
