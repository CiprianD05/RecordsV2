using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordsDbLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddedCitizenToDocumentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Citizens_CitizenId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Documents",
                newName: "Path");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DocumentTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CitizenId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Citizens_CitizenId",
                table: "Documents",
                column: "CitizenId",
                principalTable: "Citizens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Citizens_CitizenId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Documents",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DocumentTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "CitizenId",
                table: "Documents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Citizens_CitizenId",
                table: "Documents",
                column: "CitizenId",
                principalTable: "Citizens",
                principalColumn: "Id");
        }
    }
}
