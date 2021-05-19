using NServiceBus;
using NServiceBus.Logging;
using Shared.Commands.Vendor;
using Shared.Events.Vendor;
using System.Threading.Tasks;

namespace Vendor.Application.Handlers
{
    public class CancelVendorOrderHandler : IHandleMessages<CancelVendorOrderCommand>
    {
        private static readonly ILog _log = LogManager.GetLogger<CancelVendorOrderHandler>();

        public Task Handle(CancelVendorOrderCommand message, IMessageHandlerContext context)
        {
            _log.Info($"Vendor => Pedido Cancelado no fornecedor: {message.CorrelationId}");

            return context.Publish(new VendorOrderCanceled(message.CorrelationId));
        }
    }
}
