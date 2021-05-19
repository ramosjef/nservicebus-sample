using NServiceBus;
using System;

namespace Shared.Events.Vendor
{
    public class VendorOrderSent : IEvent
    {
        public VendorOrderSent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; set; }
    }
}
