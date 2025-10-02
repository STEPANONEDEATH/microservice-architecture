namespace Api.Dto;

public class CreateRequest
{
    public record CreateBookingRequest(Guid UserId, Guid ListingId, DateTime StartDate, DateTime EndDate);
}