import { mapEnumToOptions } from '@abp/ng.core';

export enum PayrollRecordStatus {
  Pending = 0,
  Processed = 1,
}

export const payrollRecordStatusOptions = mapEnumToOptions(PayrollRecordStatus);
