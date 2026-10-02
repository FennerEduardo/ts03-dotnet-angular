import { inject } from '@angular/core';
import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { CommandName, CommandResult } from '../api/ejecucion-de-saga-y-manejo-de-comandos-cqrs-en-net8-con-angular-client';
import { EJECUCION_DE_SAGA_Y_MANEJO_DE_COMANDOS_CQRS_EN_NET8_CON_ANGULAR_CLIENT } from '../api/client.token';

interface EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularStateModel {
  events: CommandResult[];
  loading: boolean;
  error: string | null;
}

const newKey = () => globalThis.crypto?.randomUUID?.() ?? Math.random().toString(36).slice(2);

export const EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularStore = signalStore(
  { providedIn: 'root' },
  withState<EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularStateModel>({ events: [], loading: false, error: null }),
  withMethods((store, client = inject(EJECUCION_DE_SAGA_Y_MANEJO_DE_COMANDOS_CQRS_EN_NET8_CON_ANGULAR_CLIENT)) => ({
    async execute(id: string, command: CommandName, payload?: Record<string, unknown>): Promise<void> {
      patchState(store, { loading: true, error: null });
      try {
        const event = await client.execute(id, command, payload, { idempotencyKey: newKey() });
        patchState(store, { events: [...store.events(), event], loading: false });
      } catch (err) {
        patchState(store, { loading: false, error: err instanceof Error ? err.message : 'Request failed' });
      }
    },
    /** Applies an event pushed by the backend (SignalR). */
    receive(event: CommandResult): void {
      patchState(store, { events: [...store.events(), event] });
    }
  }))
);
