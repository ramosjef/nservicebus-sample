using NServiceBus;
using NServiceBus.Logging;
using Sales.Shared.Messages.Sales;
using Shared.Commands.Payment;
using Shared.Commands.Vendor;
using Shared.Events.Payment;
using Shared.Events.Sales;
using Shared.Events.Vendor;
using System;
using System.Threading.Tasks;

namespace Application.Sagas.CreateOrderSaga
{
    public class CreateOrderSaga : Saga<CreateOrderSagaData>,
        IAmStartedByMessages<CreatePreOrderCommand>,
        IHandleMessages<PaymentReceived>,
        IHandleMessages<VendorOrderSent>,
        IHandleMessages<OrderCompleted>
    {
        private static readonly ILog _log = LogManager.GetLogger<CreateOrderSaga>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CreateOrderSagaData> mapper)
        {
            mapper.ConfigureMapping<CreatePreOrderCommand>(message => message.CorrelationId)
               .ToSaga(sagaData => sagaData.CorrelationId);

            mapper.ConfigureMapping<PaymentReceived>(message => message.CorrelationId)
                .ToSaga(sagaData => sagaData.CorrelationId);

            mapper.ConfigureMapping<VendorOrderSent>(message => message.CorrelationId)
                .ToSaga(sagaData => sagaData.CorrelationId);

            mapper.ConfigureMapping<OrderCompleted>(message => message.CorrelationId)
                .ToSaga(sagaData => sagaData.CorrelationId);
        }

        public Task Handle(CreatePreOrderCommand message, IMessageHandlerContext context)
        {
            _log.Info($"Sales => Início do processo de criação do pedido: {message.CorrelationId}");

            var orderId = Guid.NewGuid();

            Data.OrderId = orderId;

            _log.Info($"Sales => Pedido de venda criado {orderId} - CorrelationId: {message.CorrelationId}");

            return context.Send(new ReceivePaymentCommand(message.CorrelationId));
        }

        public Task Handle(PaymentReceived message, IMessageHandlerContext context)
        {
            Data.Status = CreatePreOrderSagaStatus.PaymentReceived;

            _log.Info($"Sales => Pagamento recebido: {message.CorrelationId}");

            return context.Send(new CreateVendorOrderCommand(message.CorrelationId));
        }

        public async Task Handle(VendorOrderSent message, IMessageHandlerContext context)
        {
            Data.Status = CreatePreOrderSagaStatus.SentToVendor;

            _log.Info($"Sales => Pedido enviado ao fornecedor {message.CorrelationId}");

            await context.Publish(new OrderCompleted(message.CorrelationId));
        }

        public Task Handle(OrderCompleted message, IMessageHandlerContext context)
        {
            Data.Status = CreatePreOrderSagaStatus.SagaCompleted;

            _log.Info($"Sales => Pedido finalizado {message.CorrelationId}");

            MarkAsComplete();

            return Task.CompletedTask;
        }
    }
}
