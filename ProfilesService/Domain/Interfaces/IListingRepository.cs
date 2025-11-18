using ProfilesService.Domain.Entities;

namespace ProfilesService.Domain.Interfaces;

public interface IListingRepository
{
    Task AddAsync(Listing listing);
    Task<Listing?> GetByIdAsync(Guid id);
    Task<IEnumerable<Listing>> GetAvailableAsync();
    Task UpdateAsync(Listing listing);
    Task DeleteAsync(Guid id);
}