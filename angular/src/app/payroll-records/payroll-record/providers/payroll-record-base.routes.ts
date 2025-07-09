import { ABP, eLayoutType } from '@abp/ng.core';

export const PAYROLL_RECORD_BASE_ROUTES: ABP.Route[] = [
  {
    path: '/payroll-records',
    iconClass: 'fas fa-file-alt',
    name: '::Menu:PayrollRecords',
    layout: eLayoutType.application,
    requiredPolicy: 'HRManagement.PayrollRecords',
    breadcrumbText: '::PayrollRecords',
  },
];
