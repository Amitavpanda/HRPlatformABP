import { ABP, eLayoutType } from '@abp/ng.core';

export const ATTENDANCE_LOG_BASE_ROUTES: ABP.Route[] = [
  {
    path: '/attendance-logs',
    iconClass: 'fas fa-file-alt',
    name: '::Menu:AttendanceLogs',
    layout: eLayoutType.application,
    requiredPolicy: 'HRManagement.AttendanceLogs',
    breadcrumbText: '::AttendanceLogs',
  },
];
