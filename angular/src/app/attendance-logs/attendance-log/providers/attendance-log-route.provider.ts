import { inject, provideAppInitializer } from '@angular/core';
import { ABP, RoutesService } from '@abp/ng.core';
import { ATTENDANCE_LOG_BASE_ROUTES } from './attendance-log-base.routes';

export const ATTENDANCE_LOGS_ATTENDANCE_LOG_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routesService = inject(RoutesService);
  const routes: ABP.Route[] = [...ATTENDANCE_LOG_BASE_ROUTES];
  routesService.add(routes);
}
