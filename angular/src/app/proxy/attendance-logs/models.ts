import type { AttendanceStatus } from '../attendance-status.enum';
import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { EmployeeDto } from '../employees/models';

export interface AttendanceLogCreateDto {
  date?: string;
  checkInTime?: string;
  checkOutTime?: string;
  status: AttendanceStatus;
  employeeId: string;
}

export interface AttendanceLogDto extends FullAuditedEntityDto<string> {
  date?: string;
  checkInTime?: string;
  checkOutTime?: string;
  status: AttendanceStatus;
  employeeId: string;
  concurrencyStamp?: string;
}

export interface AttendanceLogExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  dateMin?: string;
  dateMax?: string;
  checkInTimeMin?: string;
  checkInTimeMax?: string;
  checkOutTimeMin?: string;
  checkOutTimeMax?: string;
  status?: AttendanceStatus;
  employeeId?: string;
}

export interface AttendanceLogUpdateDto {
  date?: string;
  checkInTime?: string;
  checkOutTime?: string;
  status: AttendanceStatus;
  employeeId: string;
  concurrencyStamp?: string;
}

export interface AttendanceLogWithNavigationPropertiesDto {
  attendanceLog: AttendanceLogDto;
  employee: EmployeeDto;
}

export interface GetAttendanceLogsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  dateMin?: string;
  dateMax?: string;
  checkInTimeMin?: string;
  checkInTimeMax?: string;
  checkOutTimeMin?: string;
  checkOutTimeMax?: string;
  status?: AttendanceStatus;
  employeeId?: string;
}
