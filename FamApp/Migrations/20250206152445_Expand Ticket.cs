using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamApp.Migrations
{
    /// <inheritdoc />
    public partial class ExpandTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreateByUserId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeadLineDate",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Priority",
                table: "Tickets",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Public",
                table: "Tickets",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateByUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DeadLineDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Public",
                table: "Tickets");
        }
    }
}
