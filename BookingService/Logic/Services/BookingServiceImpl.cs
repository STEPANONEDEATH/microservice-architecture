using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingService.Dal.Entities;
using BookingService.Dal.Interfaces;
using BookingService.Logic.Interfaces;
using BookingService.Logic.Models;

namespace BookingService.Logic.Services
{
    public class BookingServiceImpl : IBookingService
    {
        private readonly IBookingRepository _repository;

        public BookingServiceImpl(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request)
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

            await _repository.AddAsync(booking);

            return new BookingResponse
            {
                Id = booking.Id,
                UserId = booking.UserId,
                ListingId = booking.ListingId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status
            };
        }

        public async Task<BookingResponse?> GetByIdAsync(Guid id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null) return null;

            return new BookingResponse
            {
                Id = booking.Id,
                UserId = booking.UserId,
                ListingId = booking.ListingId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status
            };
        }

        public async Task<IEnumerable<BookingResponse>> GetAllAsync()
        {
            var bookings = await _repository.GetAllAsync();
            return bookings.Select(b => new BookingResponse
            {
                Id = b.Id,
                UserId = b.UserId,
                ListingId = b.ListingId,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                Status = b.Status
            });
        }
    }
}