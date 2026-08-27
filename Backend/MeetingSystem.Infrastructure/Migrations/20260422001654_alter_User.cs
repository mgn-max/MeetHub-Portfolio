using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class alter_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdFirm",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserProfile",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdFirm",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserProfile",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
