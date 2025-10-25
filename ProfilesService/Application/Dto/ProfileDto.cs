namespace ProfileService.Api.Dto
{
    public record RegisterProfileRequest(string UserId, string FullName);
    public record RegisterProfileResponse(bool Success);
    public record CheckUserExistsResponse(bool Exists);
}