using Microsoft.EntityFrameworkCore.Migrations;

namespace Vehicles.Migrations
{
    public partial class addItemsToDatabasea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleModel",
                columns: table => new
                {
                    Guid = table.Column<int>(type: "int", nullable: false),
                    MakeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abrv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModel", x => x.Guid);
                    table.ForeignKey(
                    name: "FK_VehicleModel",
                    column: x => x.MakeId,
                    principalTable: "VehicleMake",
                    principalColumn: "Guid",
                    onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleModel");
        }
    }
}
