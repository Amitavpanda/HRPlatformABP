import { inject, provideAppInitializer } from '@angular/core';
import { ABP, RoutesService } from '@abp/ng.core';
import { LEAVE_REQUEST_BASE_ROUTES } from './leave-request-base.routes';

export const LEAVE_REQUESTS_LEAVE_REQUEST_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routesService = inject(RoutesService);
  const routes: ABP.Route[] = [...LEAVE_REQUEST_BASE_ROUTES];
  routesService.add(routes);
}
