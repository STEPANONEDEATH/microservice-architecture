namespace Api.Dto;

public record CreateBookingRequest(Guid UserId, Guid ListingId, DateTime StartDate, DateTime EndDate);

public record BookingResponse(Guid Id, string Status);
