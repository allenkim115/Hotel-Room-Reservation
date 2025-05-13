using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExistingReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Set all reservations with UserId = 0 to UserId = 1 (assuming 1 is a valid admin user)
            migrationBuilder.Sql(
                "UPDATE \"Reservations\" SET \"UserId\" = 1 WHERE \"UserId\" = 0;"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No need to revert the data update
        }
    }
} 