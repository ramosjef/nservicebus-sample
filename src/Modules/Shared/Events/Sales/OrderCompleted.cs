using NServiceBus;
using System;

namespace Shared.Events.Sales
{
    public class OrderCompleted : IEvent
    {
        public OrderCompleted(Guid orderId)
        {
            CorrelationId = orderId;
        }

        public Guid CorrelationId { get; set; }
    }
}
