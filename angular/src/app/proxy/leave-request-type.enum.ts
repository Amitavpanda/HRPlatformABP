import { mapEnumToOptions } from '@abp/ng.core';

export enum LeaveRequestType {
  SickLeave = 0,
  CasualLeave = 1,
  AnnualLeave = 2,
  MaternityLeave = 3,
  UnpaidLeave = 4,
  Other = 5,
}

export const leaveRequestTypeOptions = mapEnumToOptions(LeaveRequestType);
