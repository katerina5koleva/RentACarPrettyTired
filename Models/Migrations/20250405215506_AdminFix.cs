using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    /// <summary>
    /// Represents a migration to fix the admin user's password hash in the database.
    /// </summary>
    public partial class AdminFix : Migration
    {
        /// <summary>
        /// Applies the migration by updating the password hash of a specific admin user in the AspNetUsers table.
        /// </summary>
        /// <param name="migrationBuilder">The builder used to construct the migration operations.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE AspNetUsers 
                SET PasswordHash = 'AQAAAAIAAYagAAAAEJmsXmTvQxGXj8Yzq1uXW5JZ6+7V9kKj1pZ2h3Y4vR4X5nB6r7s8W3Y2w1oA1xg=='

                WHERE Id = 'a820ccf9-54ac-4047-b4b5-48dab0dc962b'
                ");
        }

        /// <summary>
        /// Reverts the migration. This method is intentionally left empty as the operation is not reversible.
        /// </summary>
        /// <param name="migrationBuilder">The builder used to construct the migration operations.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
