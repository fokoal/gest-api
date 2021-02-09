using Microsoft.EntityFrameworkCore.Migrations;

namespace gesteco.api.domain.data.migrations
{
    public partial class update_ecocentre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Codepostal",
                table: "Ecocentre",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rue",
                table: "Ecocentre",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ville",
                table: "Ecocentre",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codepostal",
                table: "Ecocentre");

            migrationBuilder.DropColumn(
                name: "Rue",
                table: "Ecocentre");

            migrationBuilder.DropColumn(
                name: "Ville",
                table: "Ecocentre");
        }
    }
}
