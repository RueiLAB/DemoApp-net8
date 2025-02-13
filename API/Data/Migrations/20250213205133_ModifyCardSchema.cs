using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCardSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Todos_CardId",
                table: "Todos",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Cards_CardId",
                table: "Todos",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Cards_CardId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_CardId",
                table: "Todos");
        }
    }
}
