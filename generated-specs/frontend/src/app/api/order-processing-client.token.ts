import { InjectionToken } from '@angular/core';
import { createOrderProcessingClient, OrderProcessingClient } from './order-processing-client';

/** Swap the API client per environment or in tests. */
export const ORDER_PROCESSING_CLIENT = new InjectionToken<OrderProcessingClient>('OrderProcessingClient', {
  providedIn: 'root',
  factory: () => createOrderProcessingClient()
});
