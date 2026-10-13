import { provideZonelessChangeDetection } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { describe, expect, it, vi } from 'vitest';
import { OrderProcessingPanelComponent } from './order-processing-panel.component';
import { ORDER_PROCESSING_CLIENT } from './api/order-processing-client.token';

describe('OrderProcessingPanelComponent', () => {
  it('executes a command and lists the resulting event', async () => {
    const execute = vi.fn().mockResolvedValue({ type: 'ProcessOrderCompleted', aggregateId: 'agg-1', version: 1 });
    TestBed.configureTestingModule({ imports: [OrderProcessingPanelComponent], providers: [provideZonelessChangeDetection(), { provide: ORDER_PROCESSING_CLIENT, useValue: { execute } }] });
    const fixture = TestBed.createComponent(OrderProcessingPanelComponent);
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
    TestBed.configureTestingModule({ imports: [OrderProcessingPanelComponent], providers: [provideZonelessChangeDetection(), { provide: ORDER_PROCESSING_CLIENT, useValue: { execute: vi.fn() } }] });
    const fixture = TestBed.createComponent(OrderProcessingPanelComponent);
    fixture.detectChanges();
    expect(fixture.nativeElement.querySelector('[data-test="process_order"]').disabled).toBe(true);
  });
});
