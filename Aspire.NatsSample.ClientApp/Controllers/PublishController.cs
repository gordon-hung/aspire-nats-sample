using System.Text;

using Microsoft.AspNetCore.Mvc;

using NATS.Client.Core;

namespace Aspire.NatsSample.ClientApp.Controllers;

[Route("[controller]")]
[ApiController]
public class PublishController(INatsConnection connection) : ControllerBase
{
	[HttpPost]
	public ValueTask PublishAsync(
		[FromBody] string message= "publish")
		=> connection.PublishAsync("aspire.nats.sample.publish", Encoding.UTF8.GetBytes(message));
}
