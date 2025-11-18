using Microsoft.EntityFrameworkCore;
using ProfilesService.Domain.Entities;
using ProfilesService.Domain.Interfaces;
using ProfilesService.Infrastructure.Persistence;

namespace ProfilesService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ProfilesDbContext _db;

    public UserRepository(ProfilesDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
        => await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
}