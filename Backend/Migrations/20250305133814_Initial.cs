using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "AliveGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FK_Session = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<byte>(type: "INTEGER", nullable: false),
                    CurrentTries = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AliveGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AliveGames_Sessions_FK_Session",
                        column: x => x.FK_Session,
                        principalTable: "Sessions",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AliveGames_FK_Session",
                table: "AliveGames",
                column: "FK_Session");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AliveGames");

            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
