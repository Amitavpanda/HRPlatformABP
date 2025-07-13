import { mapEnumToOptions } from '@abp/ng.core';

export enum LeaveRequestType {
  SickLeave = 0,
  PaidLeave = 1,
  UnpaidLeave = 2,
}

export const leaveRequestTypeOptions = mapEnumToOptions(LeaveRequestType);
