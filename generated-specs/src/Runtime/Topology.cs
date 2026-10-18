namespace transactionalsystemdotnetangular.Runtime;

/// <summary>
/// Event bus destinations: events are published to Exchange (RabbitMQ topic exchange / Pub/Sub or Service Bus
/// topic) and consumed from Queue (queue / subscription); dead-lettered messages go to Dlx and are read from Dlq.
/// </summary>
public sealed record Topology(string Exchange, string Queue, string Dlx, string Dlq)
{
    public static readonly Topology Default = new("transactional-system-dotnet-angular.events", "transactional-system-dotnet-angular.consumer", "transactional-system-dotnet-angular.dlx", "transactional-system-dotnet-angular.dlq");

    public static Topology ForPrefix(string prefix) => new($"{prefix}.events", $"{prefix}.consumer", $"{prefix}.dlx", $"{prefix}.dlq");
}
