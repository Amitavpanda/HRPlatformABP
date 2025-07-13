import { RoutesService, eLayoutType } from '@abp/ng.core';
import { inject, provideAppInitializer } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routes = inject(RoutesService);
  routes.add([
    {
      path: '/',
      name: '::Menu:Home',
      iconClass: 'fas fa-home',
      order: 1,
      layout: eLayoutType.application,
    },
    {
      path: '/dashboard',
      name: '::Menu:Dashboard',
      iconClass: 'fas fa-chart-line',
      order: 2,
      layout: eLayoutType.application,
      requiredPolicy: 'HRManagement.Dashboard.Host  || HRManagement.Dashboard.Tenant',
    },
    {
      path: '/attendance',
      name: 'Attendance Logs',
      iconClass: 'fas fa-calendar-check',
      order: 3,
      layout: eLayoutType.application,

    },
    {
      path: '/leave-request-initiation',
      name: 'Leave Request Initiation',
      iconClass: 'fas fa-file-alt',
      order: 4,
      layout: eLayoutType.application,
    },
    {
      path: '/hrmanagers/dashboard',
      name: 'HR Manager Dashboard',
      iconClass: 'fas fa-user-tie',
      order: 5,
      layout: eLayoutType.application,
    },
  ]);
}
