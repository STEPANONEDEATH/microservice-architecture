using System;
using MassTransit;
using Logic.Messaging.Contracts;

namespace BookingService.Sagas
{
    public class BookingStateMachine : MassTransitStateMachine<BookingState>
    {
        public State WaitingForPayment { get; private set; }

        public Event<BookingCreated> BookingCreatedEvent { get; private set; }
        public Event<PaymentProcessed> PaymentProcessedEvent { get; private set; }
        public Event<PaymentFailed> PaymentFailedEvent { get; private set; }

        public BookingStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => BookingCreatedEvent, x => x.CorrelateById(m => m.Message.BookingId));
            Event(() => PaymentProcessedEvent, x => x.CorrelateById(m => m.Message.BookingId));
            Event(() => PaymentFailedEvent, x => x.CorrelateById(m => m.Message.BookingId));

            Initially(
                When(BookingCreatedEvent)
                    .Then(context =>
                    {
                        context.Instance.BookingId = context.Data.BookingId;
                        context.Instance.UserId = context.Data.UserId;
                        context.Instance.CreatedAt = DateTime.UtcNow;
                    })
                    .ThenAsync(async ctx =>
                    {
                        // Отправляем команду ProcessPayment (оркестратор инициирует оплату)
                        await ctx.Publish(new ProcessPayment(ctx.Instance.BookingId, ctx.Instance.UserId, ctx.Data.Amount));
                    })
                    .TransitionTo(WaitingForPayment)
            );

            During(WaitingForPayment,
                When(PaymentProcessedEvent)
                    .Then(context =>
                    {
                        context.Instance.TransactionId = context.Data.BookingId.ToString();
                    })
                    .ThenAsync(async ctx =>
                    {
                        await ctx.Publish(new BookingConfirmed(ctx.Instance.BookingId));
                    })
                    .Finalize(),

                When(PaymentFailedEvent)
                    .ThenAsync(async ctx =>
                    {
                        await ctx.Publish(new BookingCancelled(ctx.Instance.BookingId, ctx.Data.Reason));
                    })
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }
    }
}
