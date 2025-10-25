using ProfilesService.Application.Dto;

namespace ProfilesService.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> RegisterAsync(string username, string email, string password);
}