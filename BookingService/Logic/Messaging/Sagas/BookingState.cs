using MassTransit;
using System;

namespace BookingService.Sagas
{
    public class BookingState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; } // required by MassTransit
        public string CurrentState { get; set; } = "";
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string TransactionId { get; set; }
    }
}