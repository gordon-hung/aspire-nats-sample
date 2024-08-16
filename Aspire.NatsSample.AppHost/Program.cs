using NATS.Client.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var nats = builder.AddNats("Nats");
builder.AddProject<Projects.Aspire_NatsSample_ServiceApp>("aspirenatssample-serviceapp").WithReference(nats);

builder.AddProject<Projects.Aspire_NatsSample_ClientApp>("aspirenatssample-clientapp").WithReference(nats);

builder.Build().Run();
