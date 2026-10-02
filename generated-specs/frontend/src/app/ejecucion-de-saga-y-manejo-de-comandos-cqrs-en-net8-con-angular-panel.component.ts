import { Component, inject, signal } from '@angular/core';
import { COMMANDS } from './api/ejecucion-de-saga-y-manejo-de-comandos-cqrs-en-net8-con-angular-client';
import { EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularStore } from './state/ejecucion-de-saga-y-manejo-de-comandos-cqrs-en-net8-con-angular.store';

@Component({
  selector: 'app-root',
  standalone: true,
  template: `
    <section aria-label="Ejecución de Saga y Manejo de Comandos CQRS en .NET 8 con Angular">
      <h1>Ejecución de Saga y Manejo de Comandos CQRS en .NET 8 con Angular</h1>
      <label>
        Aggregate id
        <input data-test="aggregate-id" [value]="aggregateId()" (input)="aggregateId.set($any($event.target).value)" />
      </label>
      @for (command of commands; track command) {
        <button [attr.data-test]="command" [disabled]="!aggregateId() || store.loading()" (click)="store.execute(aggregateId(), command)">{{ command }}</button>
      }
      @if (store.error()) {
        <p role="alert">{{ store.error() }}</p>
      }
      <ul aria-label="events">
        @for (e of store.events(); track e.aggregateId + '-' + e.version) {
          <li>{{ e.type }} v{{ e.version }}</li>
        }
      </ul>
    </section>
  `
})
export class EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularPanelComponent {
  readonly store = inject(EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularStore);
  readonly commands = COMMANDS;
  readonly aggregateId = signal('');
}
