import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AttendanceLogsEmployeeRoutingModule } from './attendance-logs-employee-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AttendanceLogsEmployeeComponent } from './components/attendance-logs.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AttendanceLogsEmployeeRoutingModule,
    SharedModule,
    AttendanceLogsEmployeeComponent, // Importing the standalone component
  ],
})
export class AttendanceLogEmployeesModule {}
