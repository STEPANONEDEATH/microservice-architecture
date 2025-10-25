using ProfilesService.Domain.Common;

namespace ProfilesService.Domain.Entities;

public class Booking : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ListingId { get; set; }
    public string Status { get; set; } = "pending";
    public decimal DepositAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}