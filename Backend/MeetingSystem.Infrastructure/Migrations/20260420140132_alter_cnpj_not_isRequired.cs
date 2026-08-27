using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class alter_cnpj_not_isRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Firms_Cnpj",
                table: "Firms");

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Firms",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);

            migrationBuilder.CreateIndex(
                name: "IX_Firms_Cnpj",
                table: "Firms",
                column: "Cnpj",
                unique: true,
                filter: "[Cnpj] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Firms_Cnpj",
                table: "Firms");

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Firms",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Firms_Cnpj",
                table: "Firms",
                column: "Cnpj",
                unique: true);
        }
    }
}
