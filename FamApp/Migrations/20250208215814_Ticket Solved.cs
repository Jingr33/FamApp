using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamApp.Migrations
{
    /// <inheritdoc />
    public partial class TicketSolved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Solved",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Solved",
                table: "Tickets");
        }
    }
}
