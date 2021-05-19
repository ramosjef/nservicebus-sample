using NServiceBus;
using System;

namespace Shared.Commands.Vendor
{
    public class CreateVendorOrderCommand : ICommand
    {
        public CreateVendorOrderCommand(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; set; }
    }
}
