using transactionalsystemdotnetangular.Domain.Kernel;
using Xunit;

namespace transactionalsystemdotnetangular.Tests.Domain;

public class OrderProcessingAggregateTests
{
    [Fact]
    public void Starts_in_the_initial_state_with_no_events()
    {
        var aggregate = new OrderProcessingAggregate("agg-1");
        Assert.Equal(OrderProcessingStates.PENDING, aggregate.State);
        Assert.Equal(0, aggregate.Version);
        Assert.Empty(aggregate.PendingEvents);
    }

    [Fact]
    public void Rejects_an_aggregate_without_id()
    {
        Assert.Throws<DomainValidationException>(() => new OrderProcessingAggregate(""));
    }

    [Fact]
    public void ProcessOrder_records_ProcessOrderCompleted_and_bumps_the_version()
    {
        var aggregate = new OrderProcessingAggregate("agg-1");
        var @event = aggregate.ProcessOrder(new OrderProcessingCommand("agg-1"));
        Assert.Equal(OrderProcessingEventTypes.ProcessOrderCompleted, @event.Type);
        Assert.Equal(1, @event.Version);
        Assert.Equal(1, aggregate.Version);
        Assert.Single(aggregate.PendingEvents);
    }

    [Fact]
    public void ProcessOrder_rejects_a_command_without_id()
    {
        var aggregate = new OrderProcessingAggregate("agg-1");
        Assert.Throws<DomainValidationException>(() => aggregate.ProcessOrder(new OrderProcessingCommand("")));
        Assert.Empty(aggregate.PendingEvents);
    }
}
