import { provideZonelessChangeDetection } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { describe, expect, it, vi, type Mock } from 'vitest';
import { EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularStore } from './ejecucion-de-saga-y-manejo-de-comandos-cqrs-en-net8-con-angular.store';
import { EJECUCION_DE_SAGA_Y_MANEJO_DE_COMANDOS_CQRS_EN_NET8_CON_ANGULAR_CLIENT } from '../api/client.token';

describe('EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularStore', () => {
  function setup(execute: Mock) {
    TestBed.configureTestingModule({ providers: [provideZonelessChangeDetection(), { provide: EJECUCION_DE_SAGA_Y_MANEJO_DE_COMANDOS_CQRS_EN_NET8_CON_ANGULAR_CLIENT, useValue: { execute } }] });
    return TestBed.inject(EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularStore);
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
