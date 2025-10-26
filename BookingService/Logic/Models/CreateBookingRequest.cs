using System;

namespace BookingService.Logic.Models
{
    public class CreateBookingRequest
    {
        public Guid UserId { get; set; }
        public Guid ListingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}