using NServiceBus;
using NServiceBus.Logging;
using Shared.Commands.Vendor;
using Shared.Events.Vendor;
using System.Threading.Tasks;

namespace Vendor.Application.Handlers
{
    public class SendVendorOrderHandler : IHandleMessages<CreateVendorOrderCommand>
    {
        private static readonly ILog _log = LogManager.GetLogger<SendVendorOrderHandler>();

        public Task Handle(CreateVendorOrderCommand message, IMessageHandlerContext context)
        {
            _log.Info($"Vendor => Pedido enviado ao fornecedor: {message.CorrelationId}");

            return context.Publish(new VendorOrderSent(message.CorrelationId));
        }
    }
}
