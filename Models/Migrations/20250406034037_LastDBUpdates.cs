using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    /// <summary>
    /// Represents a migration that applies the last database updates for the RentACar application.
    /// This migration modifies the "AspNetUsers" table by altering columns and adding unique indexes.
    /// </summary>
    public partial class LastDBUpdates : Migration
    {
        /// <summary>
        /// Applies the migration changes to the database.
        /// - Alters the "UserName" column to make it non-nullable with a default value and a maximum length of 256.
        /// - Alters the "NIN" column to make it non-nullable and sets its type to "nvarchar(450)".
        /// - Adds unique indexes for the "Email", "NIN", and "UserName" columns in the "AspNetUsers" table.
        /// </summary>
        /// <param name="migrationBuilder">The builder used to define the migration operations.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NIN",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NIN",
                table: "AspNetUsers",
                column: "NIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true);
        }

        /// <summary>
        /// Reverts the migration changes from the database.
        /// - Removes the unique indexes for the "Email", "NIN", and "UserName" columns in the "AspNetUsers" table.
        /// - Reverts the "UserName" column to its previous nullable state with a maximum length of 256.
        /// - Reverts the "NIN" column to its previous type of "nvarchar(max)".
        /// </summary>
        /// <param name="migrationBuilder">The builder used to define the migration operations.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NIN",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "NIN",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
