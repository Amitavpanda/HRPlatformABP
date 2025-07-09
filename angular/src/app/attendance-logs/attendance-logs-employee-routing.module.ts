import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AttendanceLogsEmployeeComponent } from './components/attendance-logs.component';

const routes: Routes = [
  {
    path: '',
    component: AttendanceLogsEmployeeComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AttendanceLogsEmployeeRoutingModule {}
