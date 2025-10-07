using BookingService.Logic.Interfaces;
using BookingService.Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(Guid id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            var result = await _bookingService.CreateBookingAsync(request);
            return Ok(result);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(Guid id)
        {
            // Метод CancelBookingAsync пока отсутствует в IBookingService
            return BadRequest("Метод отмены ещё не реализован");
        }
    }
}