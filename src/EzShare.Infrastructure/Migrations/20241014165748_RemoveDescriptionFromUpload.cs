using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EzShare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDescriptionFromUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Uploads");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Uploads",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
