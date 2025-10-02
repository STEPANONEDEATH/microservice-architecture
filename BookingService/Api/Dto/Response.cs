namespace Api.Dto;

public class Response
{
    public record BookingResponse(Guid Id, string Status);
}