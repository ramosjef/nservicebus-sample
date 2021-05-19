using NServiceBus;
using System;

namespace Shared.Commands.Payment
{
    public class CancelPaymentCommand : ICommand
    {
        public CancelPaymentCommand(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; set; }
    }
}
