/* ==========================================================================
   Generated Strongly-Typed DDD Contracts & Base Classes
   Feature: Ejecución de Saga y Manejo de Comandos CQRS en .NET 8 con Angular
   Namespace: transactionalsystemdotnetangular.Domain.OrderProcessing
   ========================================================================== */

namespace transactionalsystemdotnetangular.Domain.OrderProcessing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// --------------------------------------------------------------------------
// 1. Core DDD Interfaces & Base Classes
// --------------------------------------------------------------------------
public interface IDomainEvent
{
    Guid EventId { get; }
    DateTime OccurredOn { get; }
    string EventType { get; }
}

public abstract class AggregateRoot<TId>
{
    public TId Id { get; protected set; } = default!;
    public long Version { get; protected set; }
    private readonly List<IDomainEvent> _uncommittedEvents = new();

    public IReadOnlyCollection<IDomainEvent> GetUncommittedEvents() => _uncommittedEvents.AsReadOnly();
    public void ClearUncommittedEvents() => _uncommittedEvents.Clear();

    protected void ApplyChange(IDomainEvent @event)
    {
        _uncommittedEvents.Add(@event);
        Version++;
    }
}

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType()) return false;
        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
    }
}

// --------------------------------------------------------------------------
// 2. Strongly-Typed Domain Event Records
// --------------------------------------------------------------------------
public record OrderProcessingProcessedEvent(
    Guid EventId,
    DateTime OccurredOn,
    Guid AggregateId,
    string Details
) : IDomainEvent { public string EventType => nameof(OrderProcessingProcessedEvent); }

// --------------------------------------------------------------------------
// 3. Strongly-Typed Command & Query Records
// --------------------------------------------------------------------------
public record CommandResult(bool Success, string Message, Guid? EntityId);

public record CreateOrderProcessingCommand(
    Guid RequestId,
    Guid TenantId,
    DateTime Timestamp,
    string ReferenceCode,
    decimal Amount
);

public record OrderProcessingReadModel(Guid Id, string ReferenceCode, decimal Amount, string Status, DateTime UpdatedAt);

public record GetOrderProcessingQuery(
    Guid OrderProcessingId,
    Guid TenantId
);

// --------------------------------------------------------------------------
// 4. Strongly-Typed Domain Repository Port
// --------------------------------------------------------------------------
public interface IOrderProcessingRepository
{
    Task<OrderProcessingAggregate?> FindByIdAsync(Guid id);
    Task SaveAsync(OrderProcessingAggregate entity);
    Task DeleteAsync(Guid id);
}

public class OrderProcessingAggregate : AggregateRoot<Guid>
{
    public string ReferenceCode { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }

    public OrderProcessingAggregate() { }

    public OrderProcessingAggregate(Guid id, string referenceCode, decimal amount)
    {
        Id = id;
        ReferenceCode = referenceCode;
        Amount = amount;
        ApplyChange(new OrderProcessingProcessedEvent(Guid.NewGuid(), DateTime.UtcNow, Id, $"Created OrderProcessing"));
    }
}

// --------------------------------------------------------------------------
// 5. Application Event Publisher Port
// --------------------------------------------------------------------------
public interface IEventPublisher
{
    Task PublishAsync(IDomainEvent domainEvent);
}
