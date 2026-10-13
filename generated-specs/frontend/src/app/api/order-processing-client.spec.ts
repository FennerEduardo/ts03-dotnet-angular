import { describe, expect, it, vi } from 'vitest';
import { ApiError, COMMANDS, createOrderProcessingClient, newTraceparent } from './order-processing-client';

function fakeFetch(status: number, body: unknown) {
  return vi.fn(async (_url: RequestInfo | URL, _init?: RequestInit) => new Response(JSON.stringify(body), { status, headers: { 'Content-Type': 'application/json' } }));
}

describe('OrderProcessing API client', () => {
  it('exposes one method per domain command', () => {
    expect(COMMANDS).toEqual(['process_order']);
  });

  it('posts the command with the bearer token and idempotency key', async () => {
    const fetch = fakeFetch(202, { status: 'created', eventType: 'ProcessOrderCompleted', aggregateId: 'agg-1', version: 1 });
    const client = createOrderProcessingClient({ baseUrl: 'https://api.test/', token: 'jwt-1', fetch });

    const result = await client.processOrder('agg-1', { amount: 100 }, { idempotencyKey: 'key-1' });

    expect(result).toEqual({ type: 'ProcessOrderCompleted', aggregateId: 'agg-1', version: 1, status: 'created' });
    const [url, init] = fetch.mock.calls[0];
    expect(url).toBe('https://api.test/api/v1/order-processing/agg-1/process_order');
    expect(init?.method).toBe('POST');
    expect((init?.headers as Record<string, string>).Authorization).toBe('Bearer jwt-1');
    expect((init?.headers as Record<string, string>)['X-Tenant-Id']).toBeUndefined();
    expect((init?.headers as Record<string, string>)['Idempotency-Key']).toBe('key-1');
    expect(JSON.parse(String(init?.body))).toEqual({ amount: 100 });
  });

  it('sends a W3C traceparent that starts a new trace per request', async () => {
    const fetch = fakeFetch(201, { type: 'ProcessOrderCompleted', aggregateId: 'agg-1', version: 1 });
    const client = createOrderProcessingClient({ fetch });

    await client.execute('agg-1', 'process_order');
    await client.execute('agg-1', 'process_order');

    const sent = fetch.mock.calls.map(([, init]) => (init?.headers as Record<string, string>).traceparent);
    for (const traceparent of sent) expect(traceparent).toMatch(/^00-[0-9a-f]{32}-[0-9a-f]{16}-01$/);
    expect(sent[0]).not.toBe(sent[1]);
    expect(newTraceparent()).toMatch(/^00-[0-9a-f]{32}-[0-9a-f]{16}-01$/);
  });

  it('propagates the caller trace context or none when disabled', async () => {
    const traced = fakeFetch(201, {});
    const parent = '00-4bf92f3577b34da6a3ce929d0e0e4736-00f067aa0ba902b7-01';
    await createOrderProcessingClient({ fetch: traced, traceparent: () => parent }).execute('agg-1', 'process_order');
    expect((traced.mock.calls[0][1]?.headers as Record<string, string>).traceparent).toBe(parent);

    const untraced = fakeFetch(201, {});
    await createOrderProcessingClient({ fetch: untraced, traceparent: false }).execute('agg-1', 'process_order');
    expect((untraced.mock.calls[0][1]?.headers as Record<string, string>).traceparent).toBeUndefined();
  });

  it('raises ApiError with the server detail on failure', async () => {
    const client = createOrderProcessingClient({ fetch: fakeFetch(422, { detail: 'Command id is required' }) });
    await expect(client.execute('agg-1', 'process_order')).rejects.toEqual(new ApiError(422, 'Command id is required'));
  });
});
