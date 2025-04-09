using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models;

namespace RentACar.Data;

/// <summary>
/// Represents the database context for the Rent-A-Car system.
/// Inherits from IdentityDbContext to include identity-related tables.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<User>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to configure the database context.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the DbSet for cars available for rent.
    /// </summary>
    public DbSet<Auto> Autos { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for rental requests.
    /// </summary>
    public DbSet<Request> Requests { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for users in the system.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for booking periods of cars.
    /// </summary>
    public DbSet<BookingPeriod> BookingPeriods { get; set; }

    /// <summary>
    /// Configures the model and relationships for the database context.
    /// </summary>
    /// <param name="builder">The model builder used to configure the database schema.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure relationships for Request and Auto
        builder.Entity<Request>()
            .HasOne(r => r.Auto)
            .WithMany(a => a.Requests)
            .HasForeignKey(r => r.AutoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure relationships for Request and User
        builder.Entity<Request>()
            .HasOne(r => r.User)
            .WithMany(u => u.Requests)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure unique constraints for User properties
        builder.Entity<User>()
           .HasIndex(u => u.NIN)
           .IsUnique();

        builder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        builder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        // Constants for seeded data
        const string adminUserId = "a820ccf9-54ac-4047-b4b5-48dab0dc962b";
        const string adminRoleId = "a23a7ee8-beb5-4238-ad8a-88d54b3c3d29";
        const string userRoleId = "a23a7ee8-beb5-4238-ad8a-88d54b3c3d28";
        const string adminConcurrencyStamp = "12345678-1234-1234-1234-123456789012";
        const string userConcurrencyStamp = "12345678-1234-1234-1234-123456789013";
        const string adminPasswordHash = "AQAAAAIAAYagAAAAEJmsXmTvQxGXj8Yzq1uXW5JZ6+7V9kKj1pZ2h3Y4vR4X5nB6r7s8W3Y2w1oA1xg=="; // Hash for "Abc123!"

        // Seed Admin role
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR",
            Id = adminRoleId,
            ConcurrencyStamp = adminConcurrencyStamp
        });

        // Seed User role
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "BasicUser",
            NormalizedName = "BASICUSER",
            Id = userRoleId,
            ConcurrencyStamp = userConcurrencyStamp
        });

        // Seed Admin user
        builder.Entity<User>().HasData(new User
        {
            Id = adminUserId,
            Email = "admin@admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            EmailConfirmed = true,
            UserName = "admin@admin.com",
            NormalizedUserName = "ADMIN@ADMIN.COM",
            PasswordHash = adminPasswordHash,
            SecurityStamp = string.Empty,
            ConcurrencyStamp = adminConcurrencyStamp,
            PhoneNumber = "+1234567890",
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnd = null,
            LockoutEnabled = true,
            AccessFailedCount = 0,
            Firstname = "Admin",
            Surname = "User",
            NIN = "ADMIN123456"
        });

        // Assign Admin user to Admin role
        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = adminRoleId,
            UserId = adminUserId
        });
    }
}
