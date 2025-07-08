import { mapEnumToOptions } from '@abp/ng.core';

export enum AttendanceStatus {
  Present = 0,
  Absent = 1,
  OnLeave = 2,
  HalfDay = 3,
}

export const attendanceStatusOptions = mapEnumToOptions(AttendanceStatus);
