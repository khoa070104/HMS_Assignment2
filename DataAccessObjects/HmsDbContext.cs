using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Microsoft.Extensions.Configuration;

namespace DataAccessObjects
{
    public class HmsDbContext : DbContext
    {
        public HmsDbContext()
        {
        }
        public HmsDbContext(DbContextOptions<HmsDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

        }

        public DbSet<RoomInformation> RoomInformations { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BookingReservation> BookingReservations { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomInformation>()
                .HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeID);

            modelBuilder.Entity<BookingReservation>()
                .HasOne(br => br.Customer)
                .WithMany(c => c.BookingReservations)
                .HasForeignKey(br => br.CustomerID);

            modelBuilder.Entity<BookingDetail>()
                .HasOne(bd => bd.BookingReservation)
                .WithMany(br => br.BookingDetails)
                .HasForeignKey(bd => bd.BookingReservationID);

            modelBuilder.Entity<BookingDetail>()
                .HasOne(bd => bd.Room)
                .WithMany(r => r.BookingDetails)
                .HasForeignKey(bd => bd.RoomID);
        }
    }
}
