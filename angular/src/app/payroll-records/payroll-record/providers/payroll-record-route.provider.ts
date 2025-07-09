import { inject, provideAppInitializer } from '@angular/core';
import { ABP, RoutesService } from '@abp/ng.core';
import { PAYROLL_RECORD_BASE_ROUTES } from './payroll-record-base.routes';

export const PAYROLL_RECORDS_PAYROLL_RECORD_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routesService = inject(RoutesService);
  const routes: ABP.Route[] = [...PAYROLL_RECORD_BASE_ROUTES];
  routesService.add(routes);
}
