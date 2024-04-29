using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szoftverfejlesztés_dotnet_hw.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Entities_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Groupname",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Groupname",
                table: "Groups",
                column: "Groupname",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Groups_Groupname",
                table: "Groups");

            migrationBuilder.AlterColumn<string>(
                name: "Groupname",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
