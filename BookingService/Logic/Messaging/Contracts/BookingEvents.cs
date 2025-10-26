using System;

namespace Logic.Messaging.Contracts
{
    public record BookingCreated(Guid BookingId, Guid UserId, Guid ListingId, decimal Amount);
    public record PaymentProcessed(Guid BookingId);
    public record PaymentFailed(Guid BookingId, string Reason);

    // Новые команды/события
    public record ProcessPayment(Guid BookingId, Guid UserId, decimal Amount);
    public record BookingConfirmed(Guid BookingId);
    public record BookingCancelled(Guid BookingId, string Reason);
}
