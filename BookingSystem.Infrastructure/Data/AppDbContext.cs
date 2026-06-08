using BookingSystem.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ConfigureRelations(builder);

            // Soft Delete Global Filter
            builder.Entity<Business>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Service>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Booking>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Availability>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Review>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Payment>().HasQueryFilter(x => !x.IsDeleted);

            // Decimal Precision
            builder.Entity<Service>()
                .Property(s => s.Price)
                .HasPrecision(10, 2);

            builder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(10, 2);
        }

        private void ConfigureRelations(ModelBuilder builder)
        {
            // Business → Owner (User)
            builder.Entity<Business>()
                .HasOne(b => b.BusinessOwner)
                .WithMany()
                .HasForeignKey(b => b.BusinessOwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Service → Business
            builder.Entity<Service>()
                .HasOne(s => s.Business)
                .WithMany(b => b.Services)
                .HasForeignKey(s => s.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);

            // Booking → Customer
            builder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking → Service
            builder.Entity<Booking>()
                .HasOne(b => b.Service)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Review → Service
            builder.Entity<Review>()
                .HasOne(r => r.Service)
                .WithMany()
                .HasForeignKey(r => r.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Review → Customer
            builder.Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Availability → Service
            builder.Entity<Availability>()
                .HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Payment → Booking
            builder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithOne()
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}