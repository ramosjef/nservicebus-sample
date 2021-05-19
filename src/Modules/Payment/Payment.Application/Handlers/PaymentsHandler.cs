using NServiceBus;
using NServiceBus.Logging;
using Shared.Commands.Payment;
using Shared.Events.Payment;
using System.Threading.Tasks;

namespace Payment.Application.Handlers
{
    public class PaymentsHandler :
        IHandleMessages<ReceivePaymentCommand>,
        IHandleMessages<CancelPaymentCommand>
    {
        private static readonly ILog _log = LogManager.GetLogger<PaymentsHandler>();

        public Task Handle(ReceivePaymentCommand message, IMessageHandlerContext context)
        {
            _log.Info($"Payment => Pagamento Recebido: {message.CorrelationId}");

            return context.Publish(new PaymentReceived(message.CorrelationId));
        }

        public Task Handle(CancelPaymentCommand message, IMessageHandlerContext context)
        {
            _log.Info($"Payment => Pagamento Cancelado: {message.CorrelationId}");

            return context.Publish(new PaymentCanceled(message.CorrelationId));
        }
    }
}
