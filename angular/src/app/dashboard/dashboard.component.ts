import { Component } from '@angular/core';

@Component({
  standalone: false,
  selector: 'app-dashboard',
  template: `
    <app-host-dashboard *abpPermission="'HRManagement.Dashboard.Host'" />
    <app-tenant-dashboard *abpPermission="'HRManagement.Dashboard.Tenant'" />
  `,
})
export class DashboardComponent {}
