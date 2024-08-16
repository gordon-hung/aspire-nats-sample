using System.Text;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

using NATS.Client.Core;

namespace Aspire.NatsSample.ClientApp.Controllers;

[Route("[controller]")]
[ApiController]
public class RequestController(INatsConnection connection) : ControllerBase
{
	[HttpPost]
	public async ValueTask<MessageReply?> RequestAsync(
			 [FromBody] string message = "request")
	{
		var mesg = await connection.RequestAsync<ReadOnlyMemory<byte>, ReadOnlyMemory<byte>>(subject: "aspire.nats.sample.reply", data: Encoding.UTF8.GetBytes(message)).ConfigureAwait(false);
		var json = Encoding.UTF8.GetString(mesg.Data.Span);
		return JsonSerializer.Deserialize<MessageReply>(json);
	}

	public record MessageReply
	{
		public DateTimeOffset LogAt { get; set; }
		public string RequestSubject { get; set; } = default!;
		public string RequestMessage { get; set; } = default!;
		public string ReceiveSubject { get; set; } = default!;
		public string ReceiveGroup { get; set; } = default!;
		public Guid ReceiveToken { get; set; } = default!;
	}
}
