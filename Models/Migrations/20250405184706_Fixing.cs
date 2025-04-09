using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    /// <summary>
    /// Represents a migration that modifies the database schema by renaming the "BookingPeriod" table to "BookingPeriods",
    /// updating related foreign keys and indexes, and removing the "Number" column from the "AspNetUsers" table.
    /// </summary>
    public partial class Fixing : Migration
    {
        /// <summary>
        /// Defines the operations to apply the migration.
        /// This method:
        /// - Drops the foreign key constraint between "BookingPeriod" and "Autos".
        /// - Drops the primary key of the "BookingPeriod" table.
        /// - Removes the "Number" column from the "AspNetUsers" table.
        /// - Renames the "BookingPeriod" table to "BookingPeriods".
        /// - Renames the index associated with the "AutoId" column.
        /// - Adds a new primary key to the renamed "BookingPeriods" table.
        /// - Re-establishes the foreign key constraint with the updated table name.
        /// </summary>
        /// <param name="migrationBuilder">The builder used to define the migration operations.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPeriod_Autos_AutoId",
                table: "BookingPeriod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingPeriod",
                table: "BookingPeriod");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "BookingPeriod",
                newName: "BookingPeriods");

            migrationBuilder.RenameIndex(
                name: "IX_BookingPeriod_AutoId",
                table: "BookingPeriods",
                newName: "IX_BookingPeriods_AutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingPeriods",
                table: "BookingPeriods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPeriods_Autos_AutoId",
                table: "BookingPeriods",
                column: "AutoId",
                principalTable: "Autos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <summary>
        /// Reverts the operations defined in the <see cref="Up"/> method.
        /// This method:
        /// - Drops the foreign key constraint between "BookingPeriods" and "Autos".
        /// - Drops the primary key of the "BookingPeriods" table.
        /// - Renames the "BookingPeriods" table back to "BookingPeriod".
        /// - Renames the index associated with the "AutoId" column back to its original name.
        /// - Adds the "Number" column back to the "AspNetUsers" table.
        /// - Re-establishes the primary key for the "BookingPeriod" table.
        /// - Re-establishes the foreign key constraint with the original table name.
        /// </summary>
        /// <param name="migrationBuilder">The builder used to define the migration operations.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPeriods_Autos_AutoId",
                table: "BookingPeriods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingPeriods",
                table: "BookingPeriods");

            migrationBuilder.RenameTable(
                name: "BookingPeriods",
                newName: "BookingPeriod");

            migrationBuilder.RenameIndex(
                name: "IX_BookingPeriods_AutoId",
                table: "BookingPeriod",
                newName: "IX_BookingPeriod_AutoId");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingPeriod",
                table: "BookingPeriod",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPeriod_Autos_AutoId",
                table: "BookingPeriod",
                column: "AutoId",
                principalTable: "Autos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
