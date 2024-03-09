using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assessment_App.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabaseTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Borders",
                table: "Countries");

            migrationBuilder.CreateTable(
                name: "Border",
                columns: table => new
                {
                    BorderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Border", x => x.BorderId);
                    table.ForeignKey(
                        name: "FK_Border_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Border_CountryId",
                table: "Border",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Border");

            migrationBuilder.AddColumn<string>(
                name: "Borders",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
