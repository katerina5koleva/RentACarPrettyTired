using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    /// <summary>
    /// This migration ensures that the "AspNetUsers" table is updated to enforce stricter data integrity rules
    /// by making certain columns non-nullable and setting default values. It also updates the existing admin user
    /// with predefined values and removes the "Discriminator" column from the table.
    /// </summary>
    public partial class FixedAdmin : Migration
    {
        /// <summary>
        /// Applies the migration by:
        /// 1. Updating the existing admin user with specific values.
        /// 2. Altering columns in the "AspNetUsers" table to make them non-nullable with default values.
        /// 3. Removing the "Discriminator" column from the "AspNetUsers" table.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder used to define the operations to apply.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update the existing admin user with predefined values
            migrationBuilder.Sql(@"
                    UPDATE AspNetUsers 
                    SET 
                        Firstname = 'Admin',
                        Surname = 'User',
                        NIN = 'ADMIN123456',
                        PhoneNumber = '+1234567890',
                        PhoneNumberConfirmed = 1
                    WHERE Id = 'a820ccf9-54ac-4047-b4b5-48dab0dc962b'
                ");

            // Alter columns to be non-nullable and set default values
            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Unknown",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "+0000000000",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NIN",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "UNKNOWN",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Firstname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Unknown",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            // Remove the "Discriminator" column from the table
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }

        /// <summary>
        /// Reverts the migration by:
        /// 1. Reverting the column changes to allow null values.
        /// 2. Adding back the "Discriminator" column with its original definition.
        /// Note: The admin user data update is not reverted as the column definitions will allow NULLs again.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder used to define the operations to revert.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert column changes to allow null values
            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NIN",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Firstname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            // Add back the "Discriminator" column
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "IdentityUser");
        }
    }
}
