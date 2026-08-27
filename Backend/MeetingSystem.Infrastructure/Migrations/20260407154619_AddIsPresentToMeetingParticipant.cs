using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsPresentToMeetingParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPresent",
                table: "MeetingParticipants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPresent",
                table: "MeetingParticipants");
        }
    }
}
