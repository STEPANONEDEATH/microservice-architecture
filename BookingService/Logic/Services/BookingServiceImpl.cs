using Api.Dtos;
using Dal.Entities;
using Dal.Interfaces;
using Logic.Interfaces;

namespace Logic.Services
{
    public class BookingServiceImpl : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingServiceImpl(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Response> CreateBookingAsync(CreateBookingRequest request)
        {
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                ListingId = request.ListingId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = "Created"
            };

            await _bookingRepository.AddAsync(booking);

            return new Response(booking.Id, booking.Status);
        }

        public async Task<Response?> GetBookingAsync(Guid id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null) return null;
            return new Response(booking.Id, booking.Status);
        }

        public async Task<bool> CancelBookingAsync(Guid id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null || booking.Status == "Cancelled" || booking.Status == "Paid")
                return false;

            booking.Status = "Cancelled";
            booking.UpdatedAt = DateTime.UtcNow;

            await _bookingRepository.UpdateAsync(booking);
            return true;
        }
    }
}