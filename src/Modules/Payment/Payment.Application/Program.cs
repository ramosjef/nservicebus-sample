using NServiceBus;
using Shared.Events.Payment;
using System;
using System.Threading.Tasks;

namespace Payment.Application
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Payments";

            var endpointConfiguration = new EndpointConfiguration("Payments");

            // Select the learning (filesystem-based) transport to communicate with other endpoints
            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PaymentReceived), "Sales");
            routing.RouteToEndpoint(typeof(PaymentCanceled), "Sales");

            // Enable monitoring errors, auditing, and heartbeats with the Particular Service Platform tools
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");

            // Enable monitoring endpoint performance
            var metrics = endpointConfiguration.EnableMetrics();
            //metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));

            // Start the endpoint
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
