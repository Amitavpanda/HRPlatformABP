import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { LeaveRequestType } from '../leave-request-type.enum';
import type { LeaveRequestStatus } from '../leave-request-status.enum';
import type { EmployeeDto } from '../employees/models';
import type { IdentityUserDto } from '../volo/abp/identity/models';

export interface GetLeaveRequestsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  leaveRequestType?: LeaveRequestType;
  startDateMin?: string;
  startDateMax?: string;
  endDateMin?: string;
  endDateMax?: string;
  leaveRequestStatus?: LeaveRequestStatus;
  requestedOnMin?: string;
  requestedOnMax?: string;
  reviewedOnMin?: string;
  reviewedOnMax?: string;
  workflowInstanceId?: string;
  employeeId?: string;
  reviewedBy?: string;
}

export interface LeaveRequestCreateDto {
  leaveRequestType: LeaveRequestType;
  startDate?: string;
  endDate?: string;
  leaveRequestStatus: LeaveRequestStatus;
  requestedOn?: string;
  reviewedOn?: string;
  workflowInstanceId?: string;
  employeeId: string;
  reviewedBy?: string;
}

export interface LeaveRequestDto extends FullAuditedEntityDto<string> {
  leaveRequestType: LeaveRequestType;
  startDate?: string;
  endDate?: string;
  leaveRequestStatus: LeaveRequestStatus;
  requestedOn?: string;
  reviewedOn?: string;
  workflowInstanceId?: string;
  employeeId: string;
  reviewedBy?: string;
  concurrencyStamp?: string;
}

export interface LeaveRequestExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  leaveRequestType?: LeaveRequestType;
  startDateMin?: string;
  startDateMax?: string;
  endDateMin?: string;
  endDateMax?: string;
  leaveRequestStatus?: LeaveRequestStatus;
  requestedOnMin?: string;
  requestedOnMax?: string;
  reviewedOnMin?: string;
  reviewedOnMax?: string;
  workflowInstanceId?: string;
  employeeId?: string;
  reviewedBy?: string;
}

export interface LeaveRequestUpdateDto {
  leaveRequestType: LeaveRequestType;
  startDate?: string;
  endDate?: string;
  leaveRequestStatus: LeaveRequestStatus;
  requestedOn?: string;
  reviewedOn?: string;
  workflowInstanceId?: string;
  employeeId: string;
  reviewedBy?: string;
  concurrencyStamp?: string;
}

export interface LeaveRequestWithNavigationPropertiesDto {
  leaveRequest: LeaveRequestDto;
  employee: EmployeeDto;
  identityUser: IdentityUserDto;
}
