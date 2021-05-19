using NServiceBus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Sales.Shared.Messages.Sales;

namespace Sales.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .UseNServiceBus(context =>
                {
                    var endpointConfiguration = new EndpointConfiguration("Sales.Api");
                    var transport = endpointConfiguration.UseTransport<LearningTransport>();

                    transport.Routing().RouteToEndpoint(
                        assembly: typeof(CreatePreOrderCommand).Assembly,
                        destination: "Sales");

                    endpointConfiguration.SendOnly();

                    return endpointConfiguration;
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .Build()
                .Run();
        }        
    }
}
