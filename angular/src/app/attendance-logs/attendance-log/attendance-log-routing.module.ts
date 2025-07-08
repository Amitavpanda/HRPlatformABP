import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AttendanceLogComponent } from './components/attendance-log.component';

export const routes: Routes = [
  {
    path: '',
    component: AttendanceLogComponent,
    canActivate: [authGuard, permissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AttendanceLogRoutingModule {}
