using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordsDbLibrary.Migrations
{
    /// <inheritdoc />
    public partial class SplittedNameIntoFirstAndLasCitizenModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Citizens",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Citizens",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Citizens");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Citizens",
                newName: "Name");
        }
    }
}
