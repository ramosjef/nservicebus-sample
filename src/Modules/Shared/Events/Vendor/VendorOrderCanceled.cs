using NServiceBus;
using System;

namespace Shared.Events.Vendor
{
    public class VendorOrderCanceled : IEvent
    {
        public VendorOrderCanceled(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; set; }
    }
}
