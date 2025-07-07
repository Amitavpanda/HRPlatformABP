import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HRManagerComponent } from './components/hrmanager.component';

export const routes: Routes = [
  {
    path: '',
    component: HRManagerComponent,
    canActivate: [authGuard, permissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HRManagerRoutingModule {}
