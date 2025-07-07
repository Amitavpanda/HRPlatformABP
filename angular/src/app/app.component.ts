import { Component } from '@angular/core';

@Component({
  standalone: false,
  selector: 'app-root',
  template: `
    <abp-loader-bar />
    <abp-dynamic-layout />
    <abp-gdpr-cookie-consent />
    <abp-internet-status />
  `,
})
export class AppComponent {}
