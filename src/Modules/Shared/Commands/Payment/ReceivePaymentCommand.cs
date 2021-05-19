using NServiceBus;
using System;

namespace Shared.Commands.Payment
{
    public class ReceivePaymentCommand : ICommand
    {
        public ReceivePaymentCommand(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; set; }
    }
}
