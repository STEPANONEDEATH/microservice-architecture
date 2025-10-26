namespace ProfilesService.Application.Dto
{
    public record RegisterProfileRequest(string UserId, string FullName);
    public record RegisterProfileResponse(bool Success);
    public record CheckUserExistsResponse(bool Exists);
    public record UserProfileDto(string UserId, string FullName);
}