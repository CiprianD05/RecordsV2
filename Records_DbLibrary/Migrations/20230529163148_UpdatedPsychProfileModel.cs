using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordsDbLibrary.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPsychProfileModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Felony",
                table: "PsychologicalProfiles");

            migrationBuilder.DropColumn(
                name: "ImpulsiveBehaviorAndAngertManagementProblems",
                table: "PsychologicalProfiles");

            migrationBuilder.DropColumn(
                name: "PersonalAndFamilyHistory",
                table: "PsychologicalProfiles");

            migrationBuilder.DropColumn(
                name: "PersonalityTraits",
                table: "PsychologicalProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Felony",
                table: "PsychologicalProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImpulsiveBehaviorAndAngertManagementProblems",
                table: "PsychologicalProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonalAndFamilyHistory",
                table: "PsychologicalProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonalityTraits",
                table: "PsychologicalProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
