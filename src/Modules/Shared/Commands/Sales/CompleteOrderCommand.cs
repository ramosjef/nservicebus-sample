using NServiceBus;
using System;

namespace Shared.Commands.Sales
{
    public class CompleteOrderCommand : ICommand
    {
        public CompleteOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
    }
}
