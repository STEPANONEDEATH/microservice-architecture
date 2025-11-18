using ProfilesService.Domain.Entities;

namespace ProfilesService.Domain.Interfaces;

public interface IBookingRepository
{
    Task AddAsync(Booking booking);
    Task<Booking?> GetByIdAsync(Guid id);
    Task<IEnumerable<Booking>> GetByUserIdAsync(Guid userId);
    Task UpdateAsync(Booking booking);
}