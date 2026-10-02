using transactionalsystemdotnetangular.Domain.Kernel;
using Xunit;

namespace transactionalsystemdotnetangular.Tests.Domain;

public class EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularAggregateTests
{
    [Fact]
    public void Starts_in_the_initial_state_with_no_events()
    {
        var aggregate = new EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularAggregate("agg-1");
        Assert.Equal(EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularStates.PENDING, aggregate.State);
        Assert.Equal(0, aggregate.Version);
        Assert.Empty(aggregate.PendingEvents);
    }

    [Fact]
    public void Rejects_an_aggregate_without_id()
    {
        Assert.Throws<DomainValidationException>(() => new EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularAggregate(""));
    }

    [Fact]
    public void ProcessOrder_records_ProcessOrderCompleted_and_bumps_the_version()
    {
        var aggregate = new EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularAggregate("agg-1");
        var @event = aggregate.ProcessOrder(new EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommand("agg-1"));
        Assert.Equal(EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularEventTypes.ProcessOrderCompleted, @event.Type);
        Assert.Equal(1, @event.Version);
        Assert.Equal(1, aggregate.Version);
        Assert.Single(aggregate.PendingEvents);
    }

    [Fact]
    public void ProcessOrder_rejects_a_command_without_id()
    {
        var aggregate = new EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularAggregate("agg-1");
        Assert.Throws<DomainValidationException>(() => aggregate.ProcessOrder(new EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommand("")));
        Assert.Empty(aggregate.PendingEvents);
    }
}
