namespace TransactionalSystem.Domain.Contracts;

public record ProcessOrderCommand(
    Guid OrderId,
    Guid CustomerId,
    decimal TotalAmount,
    string Currency,
    string IdempotencyKey
);

public record OrderProcessedResult(
    Guid OrderId,
    string Status,
    DateTime CreatedAt,
    string CorrelationId
);

public interface IOrderSagaContract
{
    Task<OrderProcessedResult> ProcessOrderAsync(
        ProcessOrderCommand command, 
        CancellationToken cancellationToken);

    Task<bool> ValidateCustomerLimitsAsync(
        Guid customerId, 
        decimal amount, 
        CancellationToken cancellationToken);
}
