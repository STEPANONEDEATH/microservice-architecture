using System.Threading.Tasks;
using MassTransit;
using Logic.Messaging.Contracts;

namespace Logic.Messaging.Consumers
{
    public class PaymentConsumer : IConsumer<BookingCreated>
    {
        public async Task Consume(ConsumeContext<BookingCreated> context)
        {
            var booking = context.Message;

            // Например, публикуем событие оплаты
            // await context.Publish(new PaymentProcessed(booking.BookingId));

            await Task.CompletedTask;
        }
    }
}