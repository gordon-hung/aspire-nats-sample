using System.Text.Json;
using NATS.Client.Core;

namespace Aspire.NatsSample.ServiceApp.Services;

public class NatsReplyBackgroundService(
	ILogger<NatsReplyBackgroundService> logger,
	INatsConnection connection,
	string subject,
	string group) : BackgroundService
{
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await foreach (var msg in connection.SubscribeAsync<ReadOnlyMemory<byte>>(subject: subject, string.IsNullOrEmpty(group) ? null : group, cancellationToken: stoppingToken))
		{
			var data = new
			{
				LogAt = DateTimeOffset.UtcNow.ToString("O"),
				RequestSubject = msg.Subject,
				RequestMessage = System.Text.Encoding.UTF8.GetString(msg.Data.Span),
				ReceiveSubject = subject,
				ReceiveGroup = group,
				ReceiveToken = Guid.NewGuid()
			};
			logger.LogInformation("{logInformation}", JsonSerializer.Serialize(data));
			await msg.ReplyAsync(JsonSerializer.SerializeToUtf8Bytes(data), cancellationToken: stoppingToken).ConfigureAwait(false);
		}
	}
}
