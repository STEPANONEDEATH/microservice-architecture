using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.Logic.Models;

#nullable enable
namespace BookingService.Logic.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request);
        Task<BookingResponse?> GetByIdAsync(Guid id);
        Task<IEnumerable<BookingResponse>> GetAllAsync();
    }
}
#nullable restore