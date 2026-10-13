import { Component } from '@angular/core';
import { OrderProcessingPanelComponent } from './order-processing-panel.component';

/** Mounts the panel of every feature of the service. */
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [OrderProcessingPanelComponent],
  template: `
    <app-order-processing-panel />
  `
})
export class AppComponent {}
