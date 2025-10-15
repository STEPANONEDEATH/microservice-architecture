using ProfilesService.Application.Dto;
using ProfilesService.Application.Interfaces;
using ProfilesService.Domain.Entities;
using ProfilesService.Domain.Interfaces;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;

namespace ProfilesService.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<UserDto> RegisterAsync(string username, string email, string password)
    {
        // Проверяем, существует ли пользователь с таким email
        var existing = await _repo.GetByEmailAsync(email);
        if (existing is not null)
            throw new Exception("User already exists");

        // Создаем нового пользователя и хэшируем пароль
        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = HashPassword(password)
        };

        // Сохраняем пользователя в репозитории
        await _repo.AddAsync(user);

        // Возвращаем DTO пользователя
        return new UserDto(user.Id, user.Username, user.Email, user.Role, user.Balance);
    }
}