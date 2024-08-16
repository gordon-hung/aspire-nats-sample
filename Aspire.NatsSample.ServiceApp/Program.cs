using Aspire.NatsSample.ServiceApp.Services;

using NATS.Client;
using NATS.Client.Core;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddNatsClient("Nats");

builder.Services.AddHostedService(sp => ActivatorUtilities.CreateInstance<NatsBackgroundService>(sp, sp.GetRequiredService<INatsConnection>(), "aspire.nats.sample.publish", string.Empty));
builder.Services.AddHostedService(sp => ActivatorUtilities.CreateInstance<NatsReplyBackgroundService>(sp, sp.GetRequiredService<INatsConnection>(), "aspire.nats.sample.reply", string.Empty));
builder.Services.AddHostedService(sp => ActivatorUtilities.CreateInstance<NatsGroupABackgroundService>(sp, sp.GetRequiredService<INatsConnection>(), "aspire.nats.sample.group", "aspire.nats.sample.publish.group.a"));
builder.Services.AddHostedService(sp => ActivatorUtilities.CreateInstance<NatsGroupBBackgroundService>(sp, sp.GetRequiredService<INatsConnection>(), "aspire.nats.sample.group", "aspire.nats.sample.publish.group.b"));
builder.Services.AddHostedService(sp => ActivatorUtilities.CreateInstance<NatsBroadcastBackgroundService>(sp, sp.GetRequiredService<INatsConnection>(), "aspire.nats.sample.*", string.Empty));
var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
