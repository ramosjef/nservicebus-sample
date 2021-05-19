using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using Sales.Shared.Messages.Sales;
using Shared.Events.Sales;
using System;
using System.Threading.Tasks;

namespace Sales.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IMessageSession _messageSession;

        public SalesController(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var command = new CreatePreOrderCommand(Guid.NewGuid());

            await _messageSession.Send(command).ConfigureAwait(false);

            return Accepted("Parabéns, seu pedido foi recebido e já está sendo processado.");
        }
    }
}
