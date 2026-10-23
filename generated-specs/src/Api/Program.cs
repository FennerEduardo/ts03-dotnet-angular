using transactionalsystemdotnetangular.Api;
using transactionalsystemdotnetangular.Api.Workers;
using transactionalsystemdotnetangular.Infrastructure.Telemetry;
using transactionalsystemdotnetangular.Runtime;
using Npgsql;

// Application layer over the runtime kernel: POST /api/v1/order-processing/{id}/{command} with a JWT tenant,
// the CommandService (idempotency + outbox) and the outbox relay worker.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(_ => Connections.FromUrl(builder.Configuration["DATABASE_URL"]
    ?? throw new InvalidOperationException("DATABASE_URL is not set (postgres://user:password@host:5432/db)")));
builder.Services.AddSingleton(sp => new CommandService(sp.GetRequiredService<NpgsqlDataSource>()));
builder.Services.AddHostedService<OutboxRelayWorker>();
// OTLP export of the runtime's activities when OTEL_EXPORTER_OTLP_ENDPOINT is set.
builder.Services.AddServiceTelemetry(builder.Configuration, Telemetry.Source.Name, "transactional-system-dotnet-angular");

var app = builder.Build();
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapOrderProcessingEndpoints();
app.Run();

public partial class Program { } // WebApplicationFactory<Program> in the tests
