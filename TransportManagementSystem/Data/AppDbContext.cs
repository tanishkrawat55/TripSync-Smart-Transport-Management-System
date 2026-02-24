using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TransportManagementSystem.Models;

namespace TransportManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Booking → Customer
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking → Driver
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Driver)
                .WithMany()
                .HasForeignKey(b => b.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking → Vehicle
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Vehicle)
                .WithMany()
                .HasForeignKey(b => b.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
