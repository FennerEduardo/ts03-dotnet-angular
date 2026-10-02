// --------------------------------------------------------------------------
// MediatR CQRS Handlers & Read Model Event Projectors
// --------------------------------------------------------------------------
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using transactionalsystemdotnetangular.Infrastructure.Data;
using transactionalsystemdotnetangular.Domain.EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngular;

namespace transactionalsystemdotnetangular.Application.Handlers
{
    // 1. Command Handler
    public class CreateEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommandHandler : IRequestHandler<CreateEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommand, CommandResult>
    {
        private readonly ILogger<CreateEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommandHandler> _logger;
        private readonly ApplicationDbContext _dbContext;

        public CreateEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommandHandler(ILogger<CreateEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommandHandler> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(CreateEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing command to create {Feature} with ref {Ref}", "EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngular", request.ReferenceCode);
            
            // Scaffold: Replace with actual domain logic
            var entityId = Guid.NewGuid();
            
            // Atomic save through DbContext (enables Outbox pattern)
            // _dbContext.EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngulars.Add(newEntity);
            // await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(true, "EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngular created successfully (Scaffold)", entityId);
        }
    }

    // 2. Query Handler (Read Model)

    public class GetEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularQueryHandler : IRequestHandler<GetEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularQuery, EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularReadModel?>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularReadModel?> Handle(GetEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularQuery request, CancellationToken cancellationToken)
        {
            // Scaffold: Read from actual Read Model store or DbContext
            // var entity = await _dbContext.EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngulars.FindAsync(request.EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId);
            throw new Exception("Scaffold: Implement database read logic here");
        }
    }

    // 3. Event Projector (Read View Updater)
    public class EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularEventProjector : INotificationHandler<EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularProcessedEventNotification>
    {
        private readonly ILogger<EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularEventProjector> _logger;

        public EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularEventProjector(ILogger<EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularEventProjector> logger)
        {
            _logger = logger;
        }

        public async Task Handle(EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularProcessedEventNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Projecting read event {EventId} into ReadModel DB", notification.EventId);
            await Task.CompletedTask;
        }
    }

    public record EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularProcessedEventNotification(Guid EventId, Guid AggregateId, string Details) : INotification;
}
