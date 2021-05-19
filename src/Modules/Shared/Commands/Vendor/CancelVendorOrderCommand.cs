using NServiceBus;
using System;

namespace Shared.Commands.Vendor
{
    public class CancelVendorOrderCommand : ICommand
    {
        public CancelVendorOrderCommand(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; set; }
    }
}
