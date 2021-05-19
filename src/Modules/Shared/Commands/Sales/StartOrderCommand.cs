using NServiceBus;
using System;

namespace Shared.Commands.Sales
{
    public class StartOrderCommand : ICommand
    {
        public StartOrderCommand(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; set; }
    }
}