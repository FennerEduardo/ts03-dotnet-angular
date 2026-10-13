import { provideZonelessChangeDetection } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { describe, expect, it, vi, type Mock } from 'vitest';
import { OrderProcessingStore } from './order-processing.store';
import { ORDER_PROCESSING_CLIENT } from '../api/order-processing-client.token';

describe('OrderProcessingStore', () => {
  function setup(execute: Mock) {
    TestBed.configureTestingModule({ providers: [provideZonelessChangeDetection(), { provide: ORDER_PROCESSING_CLIENT, useValue: { execute } }] });
    return TestBed.inject(OrderProcessingStore);
  }

  it('records the event returned by the backend', async () => {
    const result = { type: 'ProcessOrderCompleted', aggregateId: 'agg-1', version: 1 };
    const store = setup(vi.fn().mockResolvedValue(result));
    await store.execute('agg-1', 'process_order');
    expect(store.events()).toEqual([result]);
    expect(store.error()).toBeNull();
  });

  it('keeps the error message when the command fails', async () => {
    const store = setup(vi.fn().mockRejectedValue(new Error('Command id is required')));
    await store.execute('agg-1', 'process_order');
    expect(store.error()).toBe('Command id is required');
  });
});
