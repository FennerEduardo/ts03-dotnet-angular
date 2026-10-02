import { InjectionToken } from '@angular/core';
import { createEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularClient, EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularClient } from './ejecucion-de-saga-y-manejo-de-comandos-cqrs-en-net8-con-angular-client';

/** Swap the API client per environment or in tests. */
export const EJECUCION_DE_SAGA_Y_MANEJO_DE_COMANDOS_CQRS_EN_NET8_CON_ANGULAR_CLIENT = new InjectionToken<EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularClient>('EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularClient', {
  providedIn: 'root',
  factory: () => createEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularClient()
});
