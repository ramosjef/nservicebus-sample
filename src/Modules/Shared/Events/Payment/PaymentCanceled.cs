using NServiceBus;
using System;

namespace Shared.Events.Payment
{
    public class PaymentCanceled : IEvent
    {
        public PaymentCanceled(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; set; }
    }
}
