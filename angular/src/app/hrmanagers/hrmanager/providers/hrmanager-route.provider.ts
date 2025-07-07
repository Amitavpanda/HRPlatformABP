import { inject, provideAppInitializer } from '@angular/core';
import { ABP, RoutesService } from '@abp/ng.core';
import { HRMANAGER_BASE_ROUTES } from './hrmanager-base.routes';

export const HRMANAGERS_HRMANAGER_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routesService = inject(RoutesService);
  const routes: ABP.Route[] = [...HRMANAGER_BASE_ROUTES];
  routesService.add(routes);
}
