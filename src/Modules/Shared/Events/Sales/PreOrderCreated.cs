using NServiceBus;
using System;

namespace Shared.Events.Sales
{
    public class PreOrderCreated : IEvent
    {
        public Guid CorrelationId { get; set; }
        public Guid OrderId { get; set; }

        public PreOrderCreated(Guid correlationId, Guid orderId)
        {
            CorrelationId = correlationId;
            OrderId = orderId;
        }
    }
}