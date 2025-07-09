import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { IdentityUserDto } from '../volo/abp/identity/models';

export interface EmployeeCreateDto {
  employeeNumber?: string;
  dateOfJoining?: string;
  leaveBalance: number;
  baseSalary: number;
  identityUserId?: string;
}

export interface EmployeeDto extends FullAuditedEntityDto<string> {
  items: any;
  employeeNumber?: string;
  dateOfJoining?: string;
  leaveBalance: number;
  baseSalary: number;
  identityUserId?: string;
  concurrencyStamp?: string;
}

export interface EmployeeExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  employeeNumber?: string;
  dateOfJoiningMin?: string;
  dateOfJoiningMax?: string;
  leaveBalanceMin?: number;
  leaveBalanceMax?: number;
  baseSalaryMin?: number;
  baseSalaryMax?: number;
  identityUserId?: string;
}

export interface EmployeeUpdateDto {
  employeeNumber?: string;
  dateOfJoining?: string;
  leaveBalance: number;
  baseSalary: number;
  identityUserId?: string;
  concurrencyStamp?: string;
}

export interface EmployeeWithNavigationPropertiesDto {
  employee: EmployeeDto;
  identityUser: IdentityUserDto;
}

export interface GetEmployeesInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  employeeNumber?: string;
  dateOfJoiningMin?: string;
  dateOfJoiningMax?: string;
  leaveBalanceMin?: number;
  leaveBalanceMax?: number;
  baseSalaryMin?: number;
  baseSalaryMax?: number;
  identityUserId?: string;
}
