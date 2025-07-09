import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LeaveRequestEmployeeComponent } from './components/leave-request-employee.component';

const routes: Routes = [
  { path: '', component: LeaveRequestEmployeeComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LeaveRequestEmployeeRoutingModule {}
