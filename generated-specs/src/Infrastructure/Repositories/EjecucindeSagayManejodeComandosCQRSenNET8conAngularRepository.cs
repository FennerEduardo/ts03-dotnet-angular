namespace transactionalsystemdotnetangular.Infrastructure.Repositories;

using System;
using System.Threading.Tasks;
using transactionalsystemdotnetangular.Domain.Entities;

public class EjecucindeSagayManejodeComandosCQRSenNET8conAngularRepository : IRepository<EjecucindeSagayManejodeComandosCQRSenNET8conAngular, Guid>
{
    public async Task<EjecucindeSagayManejodeComandosCQRSenNET8conAngular?> GetByIdAsync(Guid id)
    {
        await Task.CompletedTask;
        return new EjecucindeSagayManejodeComandosCQRSenNET8conAngular("REF-SAMPLE");
    }

    public async Task AddAsync(EjecucindeSagayManejodeComandosCQRSenNET8conAngular entity)
    {
        await Task.CompletedTask;
    }
}
