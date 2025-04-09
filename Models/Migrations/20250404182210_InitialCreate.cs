using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 

namespace RentACar.Data.Migrations
{
    /// <summary>
    /// Represents the initial migration for the RentACar database.
    /// This migration creates the necessary tables, columns, and relationships
    /// for the application, including seed data for roles and an admin user.
    /// </summary>
    public partial class InitialCreate : Migration
    {
        /// <summary>
        /// Defines the operations to apply the migration.
        /// This method creates the "Autos" and "Requests" tables, adds new columns
        /// to the "AspNetUsers" table, and seeds initial data for roles and users.
        /// </summary>
        /// <param name="migrationBuilder">The builder used to construct the migration operations.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NIN",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            // Create the Autos table
            migrationBuilder.CreateTable(
                name: "Autos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    PassengerSeats = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerDay = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autos", x => x.Id);
                });

            // Create the Requests table
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutoId = table.Column<int>(type: "int", nullable: false),
                    PickUpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsDeclined = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Autos_AutoId",
                        column: x => x.AutoId,
                        principalTable: "Autos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Seed initial data for roles
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                        { "a23a7ee8-beb5-4238-ad8a-88d54b3c3d28", "12345678-1234-1234-1234-123456789013", "BasicUser", "BASICUSER" },
                        { "a23a7ee8-beb5-4238-ad8a-88d54b3c3d29", "12345678-1234-1234-1234-123456789012", "Administrator", "ADMINISTRATOR" }
                });

            // Seed initial data for an admin user
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a820ccf9-54ac-4047-b4b5-48dab0dc962b", 0, "12345678-1234-1234-1234-123456789012", "IdentityUser", "admin@admin.com", true, true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEJmsXmTvQxGXj8Yzq1uXW5JZ6+7V9kKj1pZ2h3Y4vR4X5nB6r7s8W3Y2w1oA1xg==", null, false, "", false, "admin@admin.com" });

            // Assign the admin user to the Administrator role
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a23a7ee8-beb5-4238-ad8a-88d54b3c3d29", "a820ccf9-54ac-4047-b4b5-48dab0dc962b" });

            // Create indexes for foreign key relationships
            migrationBuilder.CreateIndex(
                name: "IX_Requests_AutoId",
                table: "Requests",
                column: "AutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserId",
                table: "Requests",
                column: "UserId");
        }

        /// <summary>
        /// Defines the operations to revert the migration.
        /// This method removes the "Autos" and "Requests" tables, deletes the seeded data,
        /// and drops the added columns from the "AspNetUsers" table.
        /// </summary>
        /// <param name="migrationBuilder">The builder used to construct the migration operations.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the Requests table
            migrationBuilder.DropTable(
                name: "Requests");

            // Drop the Autos table
            migrationBuilder.DropTable(
                name: "Autos");

            // Delete seeded roles
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a23a7ee8-beb5-4238-ad8a-88d54b3c3d28");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a23a7ee8-beb5-4238-ad8a-88d54b3c3d29", "a820ccf9-54ac-4047-b4b5-48dab0dc962b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a23a7ee8-beb5-4238-ad8a-88d54b3c3d29");

            // Delete seeded admin user
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a820ccf9-54ac-4047-b4b5-48dab0dc962b");

            // Drop added columns from AspNetUsers table
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NIN",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");
        }
    }
}
