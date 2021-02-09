using Microsoft.EntityFrameworkCore.Migrations;

namespace gesteco.api.domain.data.migrations
{
    public partial class initialEcocentrematiere : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ecocentre_Matiere",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEcocentre = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: false),
                    Comptable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecocentre_Matiere", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ecocentre_Matiere_Ecocentre_IdEcocentre",
                        column: x => x.IdEcocentre,
                        principalTable: "Ecocentre",
                        principalColumn: "IdEcocentre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ecocentre_Matiere_IdEcocentre",
                table: "Ecocentre_Matiere",
                column: "IdEcocentre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ecocentre_Matiere");
        }
    }
}
