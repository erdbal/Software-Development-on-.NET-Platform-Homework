using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szoftverfejlesztés_dotnet_hw.Migrations
{
    /// <inheritdoc />
    public partial class extra_seed_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Creatorname", "Groupname" },
                values: new object[] { 2, "admin", "group1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { 2, "user1", "user1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
