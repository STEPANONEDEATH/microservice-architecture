using BookingService.Api.Dtos;

namespace BookingService.Logic.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request);
        Task<BookingResponse?> GetBookingAsync(Guid id);
        Task<bool> CancelBookingAsync(Guid id);
    }
}