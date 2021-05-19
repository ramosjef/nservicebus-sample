using NServiceBus;
using System;

namespace Application.Sagas.CreateOrderSaga
{
    public class CreateOrderSagaData : ContainSagaData
    {
        public Guid CorrelationId { get; set; }
        public Guid OrderId { get; set; }
        public CreatePreOrderSagaStatus Status { get; set; }
    }

    public enum CreatePreOrderSagaStatus
    {
        PreOrderCreated,
        PaymentReceived,
        SentToVendor,
        SagaCompleted
    }
}