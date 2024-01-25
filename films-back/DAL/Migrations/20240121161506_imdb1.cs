using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class imdb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Imdb_FilmId",
                table: "Imdb",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Imdb_Films_FilmId",
                table: "Imdb",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imdb_Films_FilmId",
                table: "Imdb");

            migrationBuilder.DropIndex(
                name: "IX_Imdb_FilmId",
                table: "Imdb");
        }
    }
}
