using Microsoft.EntityFrameworkCore;
using BookingService.Dal.Entities;

namespace BookingService.Dal
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) {}

        public DbSet<Booking> Bookings { get; set; }
    }
}