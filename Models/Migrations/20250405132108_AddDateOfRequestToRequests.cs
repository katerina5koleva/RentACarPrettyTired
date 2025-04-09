using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    /// <summary>
    /// This migration adds a new column named "DateOfRequest" to the "Requests" table in the database.
    /// The column is of type DateTime and is non-nullable, with a default value of DateTime.MinValue.
    /// </summary>
    public partial class AddDateOfRequestToRequests : Migration
    {
        /// <summary>
        /// Applies the migration by adding the "DateOfRequest" column to the "Requests" table.
        /// </summary>
        /// <param name="migrationBuilder">The builder used to define database schema changes.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfRequest",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <summary>
        /// Reverts the migration by removing the "DateOfRequest" column from the "Requests" table.
        /// </summary>
        /// <param name="migrationBuilder">The builder used to define database schema changes.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfRequest",
                table: "Requests");
        }
    }
}
