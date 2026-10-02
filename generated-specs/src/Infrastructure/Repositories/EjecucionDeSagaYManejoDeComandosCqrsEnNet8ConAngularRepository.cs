namespace transactionalsystemdotnetangular.Infrastructure.Repositories;

using System;
using System.Threading.Tasks;
using transactionalsystemdotnetangular.Domain.Entities;

public class EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularRepository : IRepository<EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngular, Guid>
{
    public async Task<EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngular?> GetByIdAsync(Guid id)
    {
        await Task.CompletedTask;
        return new EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngular("REF-SAMPLE");
    }

    public async Task AddAsync(EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngular entity)
    {
        await Task.CompletedTask;
    }
}
