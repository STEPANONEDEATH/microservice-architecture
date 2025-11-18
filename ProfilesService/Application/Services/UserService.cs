using ProfilesService.Application.Dto;
using ProfilesService.Application.Interfaces;
using ProfilesService.Domain.Entities;
using ProfilesService.Domain.Interfaces;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;
using Application.Common.Synchronization;
using System;

namespace ProfilesService.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IDistributedSemaphore _semaphore;

    public UserService(IUserRepository repo, IDistributedSemaphore semaphore)
    {
        _repo = repo;
        _semaphore = semaphore;
    }

    public async Task<UserDto> RegisterAsync(string username, string email, string password)
    {
        // ключ семафора — уникален для email
        var key = $"semaphore:user:register:{email}";
        
        var acquired = await _semaphore.WaitAsync(
            key: key,
            permits: 1,
            timeout: TimeSpan.FromSeconds(5)
        );

        if (!acquired)
            throw new Exception("Registration is currently locked for this email. Try again later.");

        try
        {
            var existing = await _repo.GetByEmailAsync(email);
            if (existing is not null)
                throw new Exception("User already exists");

            // Создаем нового пользователя
            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = HashPassword(password)
            };

            await _repo.AddAsync(user);

            return new UserDto(user.Id, user.Username, user.Email, user.Role, user.Balance);
        }
        finally
        {
            // Освобождаем семафор
            await _semaphore.ReleaseAsync(key, 1);
        }
    }
}