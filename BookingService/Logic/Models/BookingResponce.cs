using System;

namespace BookingService.Logic.Models
{
    public class BookingResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ListingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}