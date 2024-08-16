using System.Text;

using Microsoft.AspNetCore.Mvc;

using NATS.Client.Core;

namespace Aspire.NatsSample.ClientApp.Controllers;

[Route("[controller]")]
[ApiController]
public class GroupController(INatsConnection connection) : ControllerBase
{
	[HttpPost]
	public ValueTask PublishAsync(
		[FromBody] string message = "group")
		=> connection.PublishAsync("aspire.nats.sample.group", Encoding.UTF8.GetBytes(message));
}
