using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gerenciamento_pedidos.api.Migrations
{
    /// <inheritdoc />
    public partial class CreateCollumIsBusy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBusy",
                table: "Tables",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBusy",
                table: "Tables");
        }
    }
}
