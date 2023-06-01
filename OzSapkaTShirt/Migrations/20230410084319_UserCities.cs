using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OzSapkaTShirt.Migrations
{
    public partial class UserCities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityCode",
                table: "AspNetUsers",
                column: "CityCode");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityCode",
                table: "AspNetUsers",
                column: "CityCode",
                principalTable: "Cities",
                principalColumn: "PlateCode",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityCode",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CityCode",
                table: "AspNetUsers");
        }
    }
}
