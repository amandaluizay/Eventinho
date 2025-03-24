using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventinho.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserInterationRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "UserInterationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "UserInterationRequests");
        }
    }
}
