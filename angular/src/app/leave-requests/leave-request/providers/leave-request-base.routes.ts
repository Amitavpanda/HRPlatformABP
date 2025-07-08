import { ABP, eLayoutType } from '@abp/ng.core';

export const LEAVE_REQUEST_BASE_ROUTES: ABP.Route[] = [
  {
    path: '/leave-requests',
    iconClass: 'fas fa-file-alt',
    name: '::Menu:LeaveRequests',
    layout: eLayoutType.application,
    requiredPolicy: 'HRManagement.LeaveRequests',
    breadcrumbText: '::LeaveRequests',
  },
];
