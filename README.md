# aspire-nats-sample
ASP.NET Core 8.0 Aspire Nats Sample

.NET Aspire is an opinionated, cloud ready stack for building observable, production ready, distributed applications. .NET Aspire is delivered through a collection of NuGet packages that handle specific cloud-native concerns. Cloud-native apps often consist of small, interconnected pieces or microservices rather than a single, monolithic code base. Cloud-native apps generally consume a large number of services, such as databases, messaging, and caching.

In this article, you learn how to use the .NET Aspire NATS integration to send logs and traces to a NATS Server. The integration supports persistent logs and traces across application restarts via configuration.

## Get started
To get started with the .NET Aspire NATS component, install the Aspire.NATS.Net NuGet package in the consuming client project.
```sh
dotnet add package Aspire.NATS.Net
```

## App host usage
To model the Nats resource in the app host, install the Aspire.Hosting.Nats NuGet package in the app host project.
```sh
dotnet add package Aspire.Hosting.Nats
```
