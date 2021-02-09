using Microsoft.EntityFrameworkCore.Migrations;

namespace gesteco.api.domain.data.migrations
{
    public partial class updateQuotaStandard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Quantite_Commerce",
                table: "Quota_Standard",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantite_Commerce",
                table: "Quota_Standard");
        }
    }
}
