using Microsoft.EntityFrameworkCore.Migrations;

namespace Veiculo.Repositorio.Migrations
{
    public partial class att_usersroles3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsersRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
