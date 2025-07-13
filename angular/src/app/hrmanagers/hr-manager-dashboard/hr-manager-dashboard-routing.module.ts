import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HrManagerDashboardComponent } from './hr-manager-dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: HrManagerDashboardComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HrManagerDashboardRoutingModule {}
