using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordsDbLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddedAbbrevietionFieldToDocumentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "DocumentTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "DocumentTypes");
        }
    }
}
