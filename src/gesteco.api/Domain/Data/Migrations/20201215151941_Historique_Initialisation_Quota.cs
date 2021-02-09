using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gesteco.api.domain.data.Migrations
{
    public partial class Historique_Initialisation_Quota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Historique_Initialisation_Quota",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateInit = table.Column<DateTime>(nullable: false),
                    DateEncours = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historique_Initialisation_Quota", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historique_Initialisation_Quota");
        }
    }
}
