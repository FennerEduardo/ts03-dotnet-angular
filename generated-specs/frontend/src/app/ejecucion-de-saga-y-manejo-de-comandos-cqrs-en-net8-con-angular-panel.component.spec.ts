import { provideZonelessChangeDetection } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { describe, expect, it, vi } from 'vitest';
import { EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularPanelComponent } from './ejecucion-de-saga-y-manejo-de-comandos-cqrs-en-net8-con-angular-panel.component';
import { EJECUCION_DE_SAGA_Y_MANEJO_DE_COMANDOS_CQRS_EN_NET8_CON_ANGULAR_CLIENT } from './api/client.token';

describe('EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularPanelComponent', () => {
  it('executes a command and lists the resulting event', async () => {
    const execute = vi.fn().mockResolvedValue({ type: 'ProcessOrderCompleted', aggregateId: 'agg-1', version: 1 });
    TestBed.configureTestingModule({ imports: [EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularPanelComponent], providers: [provideZonelessChangeDetection(), { provide: EJECUCION_DE_SAGA_Y_MANEJO_DE_COMANDOS_CQRS_EN_NET8_CON_ANGULAR_CLIENT, useValue: { execute } }] });
    const fixture = TestBed.createComponent(EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularPanelComponent);
    fixture.detectChanges();

    const input: HTMLInputElement = fixture.nativeElement.querySelector('[data-test="aggregate-id"]');
    input.value = 'agg-1';
    input.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    fixture.nativeElement.querySelector('[data-test="process_order"]').click();
    await fixture.whenStable();
    fixture.detectChanges();

    expect(fixture.nativeElement.textContent).toContain('ProcessOrderCompleted v1');
    expect(execute).toHaveBeenCalledWith('agg-1', 'process_order', undefined, expect.objectContaining({ idempotencyKey: expect.any(String) }));
  });

  it('disables commands until an aggregate id is entered', () => {
    TestBed.configureTestingModule({ imports: [EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularPanelComponent], providers: [provideZonelessChangeDetection(), { provide: EJECUCION_DE_SAGA_Y_MANEJO_DE_COMANDOS_CQRS_EN_NET8_CON_ANGULAR_CLIENT, useValue: { execute: vi.fn() } }] });
    const fixture = TestBed.createComponent(EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularPanelComponent);
    fixture.detectChanges();
    expect(fixture.nativeElement.querySelector('[data-test="process_order"]').disabled).toBe(true);
  });
});
