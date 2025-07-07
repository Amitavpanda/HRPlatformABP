import { ABP, eLayoutType } from '@abp/ng.core';

export const HRMANAGER_BASE_ROUTES: ABP.Route[] = [
  {
    path: '/hrmanagers',
    iconClass: 'fas fa-file-alt',
    name: '::Menu:HRManagers',
    layout: eLayoutType.application,
    requiredPolicy: 'HRManagement.HRManagers',
    breadcrumbText: '::HRManagers',
  },
];
