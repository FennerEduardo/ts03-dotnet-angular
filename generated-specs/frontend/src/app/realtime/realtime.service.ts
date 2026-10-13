import { Injectable, inject, signal } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import type { CommandResult } from '../api/order-processing-client';
import { OrderProcessingStore } from '../state/order-processing.store';

/** A pushed event names its aggregate type when the service hosts several features. */
type PushedEvent = CommandResult & { aggregateType?: string };

/** Pushes backend domain events (SignalR hub) into the store of their feature. */
@Injectable({ providedIn: 'root' })
export class RealtimeService {
  private readonly stores: Record<string, { receive(event: CommandResult): void }> = {
    OrderProcessing: inject(OrderProcessingStore),
  };
  private connection?: signalR.HubConnection;
  readonly connected = signal(false);

  async start(hubUrl = '/hubs/domain-events'): Promise<void> {
    this.connection = new signalR.HubConnectionBuilder().withUrl(hubUrl).withAutomaticReconnect().build();
    this.connection.on('ReceiveDomainEvent', (event: PushedEvent) => this.stores[event.aggregateType ?? 'OrderProcessing']?.receive(event));
    await this.connection.start();
    this.connected.set(true);
  }

  async stop(): Promise<void> {
    await this.connection?.stop();
    this.connected.set(false);
  }
}
