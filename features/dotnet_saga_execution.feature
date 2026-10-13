# language: es
@aggregate:OrderProcessing
Característica: Ejecución de Saga y Manejo de Comandos CQRS en .NET 8 con Angular
  Como operador de pedidos
  Quiero procesar un pedido
  Para ver su estado actualizado en la interfaz

  Escenario: Procesamiento Exitoso de Transacción en C# y Visualización en Angular
    Dado un comando "ProcessOrderCommand" con GUID de pedido válido
    Cuando MediatR envía el comando al Handler "ProcessOrderCommandHandler"
    Entonces se debe ejecutar el Outbox Interceptor de EF Core en la misma transacción SQL
    Y el servicio HTTP de Angular 17 debe recibir la respuesta mapeada a un Signal `orderSignal`
    Y el componente Angular Standalone debe actualizar la vista de la UI inmediatamente
