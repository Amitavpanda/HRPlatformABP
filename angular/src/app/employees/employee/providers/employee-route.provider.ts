import { inject, provideAppInitializer } from '@angular/core';
import { ABP, RoutesService } from '@abp/ng.core';
import { EMPLOYEE_BASE_ROUTES } from './employee-base.routes';

export const EMPLOYEES_EMPLOYEE_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routesService = inject(RoutesService);
  const routes: ABP.Route[] = [...EMPLOYEE_BASE_ROUTES];
  routesService.add(routes);
}
