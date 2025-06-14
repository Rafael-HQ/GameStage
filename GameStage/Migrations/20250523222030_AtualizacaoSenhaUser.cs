using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStage.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoSenhaUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeveloper",
                table: "Users",
                newName: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "IsDeveloper");
        }
    }
}
