using System;
using System.Threading.Tasks;
using NServiceBus;
using Shared.Events.Vendor;

namespace Vendor.Application
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            const string worker = "VendorIntegration";

            Console.Title = worker;

            var endpointConfiguration = new EndpointConfiguration(worker);

            // Select the learning (filesystem-based) transport to communicate with other endpoints
            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(VendorOrderCanceled), "Sales");
            routing.RouteToEndpoint(typeof(VendorOrderSent), "Sales");

            // Enable monitoring errors, auditing, and heartbeats with the Particular Service Platform tools
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");

            // Enable monitoring endpoint performance
            endpointConfiguration.EnableMetrics();
            //metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));

            // Start the endpoint
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
