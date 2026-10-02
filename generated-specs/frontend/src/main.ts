import { provideZonelessChangeDetection } from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';
import { EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularPanelComponent } from './app/ejecucion-de-saga-y-manejo-de-comandos-cqrs-en-net8-con-angular-panel.component';

bootstrapApplication(EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularPanelComponent, { providers: [provideZonelessChangeDetection()] }).catch(err => console.error(err));
