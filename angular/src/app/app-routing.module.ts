import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule),
    canActivate: [authGuard, permissionGuard],
  },
  {
    path: 'account',
    loadChildren: () =>
      import('@volo/abp.ng.account/public').then(m => m.AccountPublicModule.forLazy()),
  },
  {
    path: 'gdpr',
    loadChildren: () => import('@volo/abp.ng.gdpr').then(m => m.GdprModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@volo/abp.ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'language-management',
    loadChildren: () =>
      import('@volo/abp.ng.language-management').then(m => m.LanguageManagementModule.forLazy()),
  },
  {
    path: 'saas',
    loadChildren: () => import('@volo/abp.ng.saas').then(m => m.SaasModule.forLazy()),
  },
  {
    path: 'audit-logs',
    loadChildren: () =>
      import('@volo/abp.ng.audit-logging').then(m => m.AuditLoggingModule.forLazy()),
  },
  {
    path: 'openiddict',
    loadChildren: () =>
      import('@volo/abp.ng.openiddictpro').then(m => m.OpeniddictproModule.forLazy()),
  },
  {
    path: 'text-template-management',
    loadChildren: () =>
      import('@volo/abp.ng.text-template-management').then(m =>
        m.TextTemplateManagementModule.forLazy(),
      ),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
  {
    path: 'gdpr-cookie-consent',
    loadChildren: () =>
      import('./gdpr-cookie-consent/gdpr-cookie-consent.module').then(
        m => m.GdprCookieConsentModule,
      ),
  },
  {
    path: 'hrmanagers',
    loadChildren: () =>
      import('./hrmanagers/hrmanager/hrmanager.module').then(m => m.HRManagerModule),
  },
  {
    path: 'employees',
    loadChildren: () => import('./employees/employee/employee.module').then(m => m.EmployeeModule),
  },
  {
    path: 'attendance-logs',
    loadChildren: () =>
      import('./attendance-logs/attendance-log/attendance-log.module').then(
        m => m.AttendanceLogModule,
      ),
  },
  {
    path: 'leave-requests',
    loadChildren: () =>
      import('./leave-requests/leave-request/leave-request.module').then(m => m.LeaveRequestModule),
  },
  {
    path: 'payroll-records',
    loadChildren: () =>
      import('./payroll-records/payroll-record/payroll-record.module').then(
        m => m.PayrollRecordModule,
      ),
  },
  {
    path: 'attendance',
    loadChildren: () =>
      import('./attendance-logs/attendance-logs-employee.module').then(
        m => m.AttendanceLogEmployeesModule
      ),
  },
  {
    path: 'leave-request-initiation',
    loadChildren: () => import('./leave-requests/leave-request-employee/leave-request-employee.module').then(m => m.LeaveRequestEmployeeModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
