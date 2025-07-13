import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HrManagerDashboardComponent } from './hr-manager-dashboard.component';
import { HrManagerDashboardRoutingModule } from './hr-manager-dashboard-routing.module';

@NgModule({
  imports: [CommonModule, HrManagerDashboardRoutingModule, HrManagerDashboardComponent]
})
export class HrManagerDashboardModule {}
