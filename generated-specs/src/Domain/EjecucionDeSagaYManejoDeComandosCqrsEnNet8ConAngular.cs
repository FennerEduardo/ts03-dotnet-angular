namespace transactionalsystemdotnetangular.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public abstract class AggregateRoot<TId>
{
    public TId Id { get; protected set; } = default!;
    public long Version { get; protected set; }
    public string TenantId { get; protected set; } = string.Empty;
}

public abstract class ValueObject
{
}

public interface IRepository<TEntity, TId>
{
    Task<TEntity?> GetByIdAsync(TId id);
    Task AddAsync(TEntity entity);
}

public class EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngular : AggregateRoot<Guid>
{
    public string ReferenceCode { get; private set; } = string.Empty;

    public EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngular(string referenceCode, string tenantId = "default")
    {
        Id = Guid.NewGuid();
        ReferenceCode = referenceCode;
        TenantId = tenantId;
    }
}
