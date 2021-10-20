using Microsoft.EntityFrameworkCore.Migrations;

namespace Veiculo.Repositorio.Migrations
{
    public partial class att_usersroles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "UsersRoles");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsersRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersRoles");

            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "UsersRoles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
