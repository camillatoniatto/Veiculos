using Microsoft.EntityFrameworkCore.Migrations;

namespace Veiculo.Repositorio.Migrations
{
    public partial class att_usersroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Selected",
                table: "UsersRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "UsersRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_CarroId",
                table: "Reservas",
                column: "CarroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Carros_CarroId",
                table: "Reservas",
                column: "CarroId",
                principalTable: "Carros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Carros_CarroId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_CarroId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Selected",
                table: "UsersRoles");

            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "UsersRoles");
        }
    }
}
