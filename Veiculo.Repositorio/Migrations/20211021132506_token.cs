using Microsoft.EntityFrameworkCore.Migrations;

namespace Veiculo.Repositorio.Migrations
{
    public partial class token : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Carros_CarroId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersRoles_Roles_RoleId",
                table: "UsersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersRoles_Users_UserId",
                table: "UsersRoles");

            migrationBuilder.DropIndex(
                name: "IX_UsersRoles_RoleId",
                table: "UsersRoles");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_CarroId",
                table: "Reservas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_RoleId",
                table: "UsersRoles",
                column: "RoleId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRoles_Roles_RoleId",
                table: "UsersRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRoles_Users_UserId",
                table: "UsersRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
