using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACar.Data.Migrations
{
    /// <summary>
    /// This migration class is responsible for creating the Identity schema in the database.
    /// It defines the structure of tables and relationships required for ASP.NET Core Identity.
    /// </summary>
    public partial class CreateIdentitySchema : Migration
    {
        /// <summary>
        /// Defines the operations to apply the migration.
        /// This method creates the necessary tables for Identity, including roles, users, claims, logins, and tokens.
        /// </summary>
        /// <param name="migrationBuilder">The builder used to define the database schema changes.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create the AspNetRoles table to store role information
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false), // Primary key
                    Name = table.Column<string>(maxLength: 256, nullable: true), // Role name
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true), // Normalized role name
                    ConcurrencyStamp = table.Column<string>(nullable: true) // Concurrency token
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            // Create the AspNetUsers table to store user information
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false), // Primary key
                    UserName = table.Column<string>(maxLength: 256, nullable: true), // User name
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true), // Normalized user name
                    Email = table.Column<string>(maxLength: 256, nullable: true), // Email address
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true), // Normalized email
                    EmailConfirmed = table.Column<bool>(nullable: false), // Email confirmation status
                    PasswordHash = table.Column<string>(nullable: true), // Password hash
                    SecurityStamp = table.Column<string>(nullable: true), // Security stamp
                    ConcurrencyStamp = table.Column<string>(nullable: true), // Concurrency token
                    PhoneNumber = table.Column<string>(nullable: true), // Phone number
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false), // Phone number confirmation status
                    TwoFactorEnabled = table.Column<bool>(nullable: false), // Two-factor authentication status
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true), // Lockout end date
                    LockoutEnabled = table.Column<bool>(nullable: false), // Lockout enabled status
                    AccessFailedCount = table.Column<int>(nullable: false) // Failed access attempts
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            // Create the AspNetRoleClaims table to store claims associated with roles
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn), // Primary key
                    RoleId = table.Column<string>(nullable: false), // Foreign key to AspNetRoles
                    ClaimType = table.Column<string>(nullable: true), // Claim type
                    ClaimValue = table.Column<string>(nullable: true) // Claim value
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create the AspNetUserClaims table to store claims associated with users
            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn), // Primary key
                    UserId = table.Column<string>(nullable: false), // Foreign key to AspNetUsers
                    ClaimType = table.Column<string>(nullable: true), // Claim type
                    ClaimValue = table.Column<string>(nullable: true) // Claim value
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create the AspNetUserLogins table to store external login information
            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false), // Composite key part 1
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false), // Composite key part 2
                    ProviderDisplayName = table.Column<string>(nullable: true), // Display name of the provider
                    UserId = table.Column<string>(nullable: false) // Foreign key to AspNetUsers
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create the AspNetUserRoles table to store user-role relationships
            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false), // Composite key part 1
                    RoleId = table.Column<string>(nullable: false) // Composite key part 2
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create the AspNetUserTokens table to store user tokens
            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false), // Composite key part 1
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false), // Composite key part 2
                    Name = table.Column<string>(maxLength: 128, nullable: false), // Composite key part 3
                    Value = table.Column<string>(nullable: true) // Token value
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create indexes for efficient querying
            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <summary>
        /// Defines the operations to revert the migration.
        /// This method drops all the tables created in the Up method.
        /// </summary>
        /// <param name="migrationBuilder">The builder used to define the database schema changes.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
