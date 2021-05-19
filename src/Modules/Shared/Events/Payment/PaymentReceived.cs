using NServiceBus;
using System;

namespace Shared.Events.Payment
{
    public class PaymentReceived : IEvent
    {
        public PaymentReceived(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; set; }
    }
}
