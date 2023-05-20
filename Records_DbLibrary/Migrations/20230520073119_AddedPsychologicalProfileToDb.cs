using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordsDbLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddedPsychologicalProfileToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PsychologicalProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenId = table.Column<int>(type: "int", nullable: false),
                    Psychologist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Felony = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalAndFamilyHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalityTraits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImpulsiveBehaviorAndAngertManagementProblems = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologicalProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PsychologicalProfiles_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PsychologicalProfiles_CitizenId",
                table: "PsychologicalProfiles",
                column: "CitizenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PsychologicalProfiles");
        }
    }
}
