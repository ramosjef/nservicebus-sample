using NServiceBus;
using System;

namespace Sales.Shared.Messages.Sales
{
    public class CreatePreOrderCommand : ICommand
    {
        public CreatePreOrderCommand(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; set; }
    }
}
