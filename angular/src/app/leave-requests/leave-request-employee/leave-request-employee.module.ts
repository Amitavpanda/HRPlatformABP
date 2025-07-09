import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeaveRequestEmployeeRoutingModule } from './leave-request-employee-routing.module';
import { LeaveRequestEmployeeComponent } from './components/leave-request-employee.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  imports: [CommonModule, LeaveRequestEmployeeRoutingModule,LeaveRequestEmployeeComponent, ReactiveFormsModule, FormsModule, SharedModule],
})
export class LeaveRequestEmployeeModule {}

