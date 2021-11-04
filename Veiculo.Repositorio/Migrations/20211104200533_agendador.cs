using Microsoft.EntityFrameworkCore.Migrations;

namespace Veiculo.Repositorio.Migrations
{
    public partial class agendador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agendadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Minute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayMonth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayWeek = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendadores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendadores");
        }
    }
}
