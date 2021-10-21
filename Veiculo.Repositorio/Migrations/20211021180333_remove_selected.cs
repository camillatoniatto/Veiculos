using Microsoft.EntityFrameworkCore.Migrations;

namespace Veiculo.Repositorio.Migrations
{
    public partial class remove_selected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Selected",
                table: "UsersRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Selected",
                table: "UsersRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
