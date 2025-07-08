import { ABP, eLayoutType } from '@abp/ng.core';

export const EMPLOYEE_BASE_ROUTES: ABP.Route[] = [
  {
    path: '/employees',
    iconClass: 'fas fa-file-alt',
    name: '::Menu:Employees',
    layout: eLayoutType.application,
    requiredPolicy: 'HRManagement.Employees',
    breadcrumbText: '::Employees',
  },
];
