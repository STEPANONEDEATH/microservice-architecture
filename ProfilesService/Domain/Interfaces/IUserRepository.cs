using ProfilesService.Domain.Entities;

namespace ProfilesService.Domain.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByEmailAsync(string email);
}