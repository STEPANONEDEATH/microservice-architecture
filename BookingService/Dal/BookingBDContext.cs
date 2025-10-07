using BookingService.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Dal
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) {}

        public DbSet<Booking> Bookings { get; set; }
    }
}