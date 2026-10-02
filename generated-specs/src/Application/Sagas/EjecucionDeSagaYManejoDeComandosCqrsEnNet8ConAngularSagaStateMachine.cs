// --------------------------------------------------------------------------
// Saga Orchestration Pattern (.NET 8/9)
// --------------------------------------------------------------------------
using System;
using System.Threading.Tasks;
using MassTransit;

namespace transactionalsystemdotnetangular.Application.Sagas
{
    public class EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; } = string.Empty;
        public Guid EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId { get; set; }
        public string ErrorReason { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? TimeoutDeadline { get; set; }
    }

    public class EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularSagaStateMachine : MassTransitStateMachine<EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularSagaState>
    {
        public State Authorized { get; private set; } = null!;
        public State Completed { get; private set; } = null!;
        public State Compensating { get; private set; } = null!;
        public State Failed { get; private set; } = null!;

        public Event<EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularInitiatedEvent> EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularInitiated { get; private set; } = null!;
        public Event<EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularAuthorizedEvent> EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularAuthorized { get; private set; } = null!;
        public Event<EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularFailedEvent> EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularFailed { get; private set; } = null!;
        public Event<SagaTimeoutExpiredEvent> TimeoutExpired { get; private set; } = null!;

        public EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularSagaStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularInitiated, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularAuthorized, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularFailed, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => TimeoutExpired, x => x.CorrelateById(m => m.Message.CorrelationId));

            Initially(
                When(EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularInitiated)
                    .Then(context => {
                        context.Saga.EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId = context.Message.EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId;
                        context.Saga.CreatedAt = DateTime.UtcNow;
                        context.Saga.TimeoutDeadline = DateTime.UtcNow.AddMinutes(5);
                    })
                    .Publish(context => new AuthorizeEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommand { EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId = context.Saga.EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId })
                    .TransitionTo(Authorized)
            );

            During(Authorized,
                When(EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularAuthorized)
                    .Publish(context => new CompleteEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommand { EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId = context.Saga.EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId })
                    .Then(context => context.Saga.UpdatedAt = DateTime.UtcNow)
                    .TransitionTo(Completed),
                When(EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularFailed)
                    .TransitionTo(Compensating)
                    .Then(context => {
                        context.Saga.ErrorReason = context.Message.Reason;
                    })
                    .Publish(context => new CompensateEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommand { EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId = context.Saga.EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId, Reason = context.Message.Reason })
                    .TransitionTo(Failed),
                When(TimeoutExpired)
                    .TransitionTo(Compensating)
                    .Then(context => {
                        context.Saga.ErrorReason = "Saga timeout reached without authorization.";
                    })
                    .Publish(context => new CompensateEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommand { EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId = context.Saga.EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId, Reason = "Timeout" })
                    .TransitionTo(Failed)
            );
        }
    }

    public record EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularInitiatedEvent(Guid CorrelationId, Guid EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId);
    public record EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularAuthorizedEvent(Guid CorrelationId);
    public record EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularFailedEvent(Guid CorrelationId, string Reason);
    public record SagaTimeoutExpiredEvent(Guid CorrelationId);
    public record AuthorizeEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommand { public Guid EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId { get; init; } }
    public record CompleteEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommand { public Guid EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId { get; init; } }
    public record CompensateEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommand { public Guid EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularId { get; init; } public string Reason { get; init; } = string.Empty; }
}
