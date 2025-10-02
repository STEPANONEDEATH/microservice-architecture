using Microsoft.AspNetCore.Mvc;
using Api.Dto;
using Logic.Interfaces;

namespace Api.Controllers
{
    /// <summary>
    /// Контроллер для управления брони.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        /// <summary>
        /// Конструктор контроллера брони.
        /// </summary>
        /// <param name="bookingService">Сервис для работы с бронированиями.</param>
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Создать новое бронирование.
        /// </summary>
        /// <param name="request">DTO с данными для создания бронирования.</param>
        /// <returns>DTO с идентификатором и статусом бронирования.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            var booking = await _bookingService.CreateBookingAsync(request);
            return Ok(booking);
        }

        /// <summary>
        /// Получить бронирование по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор бронирования.</param>
        /// <returns>DTO с информацией о бронировании.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(Guid id)
        {
            var booking = await _bookingService.GetBookingAsync(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        /// <summary>
        /// Отменить бронирование по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор бронирования.</param>
        /// <returns>HTTP 200, если отмена успешна, иначе 400 с сообщением.</returns>
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(Guid id)
        {
            var result = await _bookingService.CancelBookingAsync(id);
            if (!result) return BadRequest("Невозможно отменить бронь");
            return Ok();
        }
    }
}
