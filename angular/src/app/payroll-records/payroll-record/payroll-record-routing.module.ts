import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PayrollRecordComponent } from './components/payroll-record.component';

export const routes: Routes = [
  {
    path: '',
    component: PayrollRecordComponent,
    canActivate: [authGuard, permissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PayrollRecordRoutingModule {}
