import { inject } from '@angular/core';
import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { CommandName, CommandResult } from '../api/order-processing-client';
import { ORDER_PROCESSING_CLIENT } from '../api/order-processing-client.token';

interface OrderProcessingStateModel {
  events: CommandResult[];
  loading: boolean;
  error: string | null;
}

const newKey = () => globalThis.crypto?.randomUUID?.() ?? Math.random().toString(36).slice(2);

export const OrderProcessingStore = signalStore(
  { providedIn: 'root' },
  withState<OrderProcessingStateModel>({ events: [], loading: false, error: null }),
  withMethods((store, client = inject(ORDER_PROCESSING_CLIENT)) => ({
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
