using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingService.Dal.Entities;
using BookingService.Dal.Interfaces;
using BookingService.Logic.Interfaces;
using BookingService.Logic.Models;
using ProfileConnectionLib;
using Microsoft.Extensions.Logging; // для логгирования

namespace BookingService.Logic.Services
{
    public class BookingServiceImpl : IBookingService
    {
        private readonly IBookingRepository _repository;
        private readonly IProfileServiceClient _profileClient;
        private readonly ILogger<BookingServiceImpl> _logger;

        public BookingServiceImpl(
            IBookingRepository repository,
            IProfileServiceClient profileClient,
            ILogger<BookingServiceImpl> logger)
        {
            _repository = repository;
            _profileClient = profileClient;
            _logger = logger;
        }

        public async Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request)
        {
            // --- Проверяем, существует ли пользователь ---
            var user = await _profileClient.GetUserAsync(request.UserId);
            if (user == null)
            {
                _logger.LogWarning("Попытка создать бронь для несуществующего пользователя {UserId}", request.UserId);
                throw new InvalidOperationException($"Пользователь {request.UserId} не найден в ProfileService.");
            }

            // --- Создаём бронь ---
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
            _logger.LogInformation("Создана новая бронь {BookingId} для пользователя {UserId}", booking.Id, booking.UserId);

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
