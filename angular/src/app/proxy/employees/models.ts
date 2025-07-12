import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { IdentityUserDto } from '../volo/abp/identity/models';

export interface EmployeeCreateDto {
  
  employeeNumber?: string;
  dateOfJoining?: string;
  paidLeaveBalance: number;
  baseSalary: number;
  unpaidLeaveBalance: number;
  sickLeaveBalance: number;
  deductionPerDay: number;
  identityUserId?: string;
}

export interface EmployeeDto extends FullAuditedEntityDto<string> {
  items : any;
  employeeNumber?: string;
  dateOfJoining?: string;
  paidLeaveBalance: number;
  baseSalary: number;
  unpaidLeaveBalance: number;
  sickLeaveBalance: number;
  deductionPerDay: number;
  identityUserId?: string;
  concurrencyStamp?: string;
}

export interface EmployeeExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  employeeNumber?: string;
  dateOfJoiningMin?: string;
  dateOfJoiningMax?: string;
  paidLeaveBalanceMin?: number;
  paidLeaveBalanceMax?: number;
  baseSalaryMin?: number;
  baseSalaryMax?: number;
  unpaidLeaveBalanceMin?: number;
  unpaidLeaveBalanceMax?: number;
  sickLeaveBalanceMin?: number;
  sickLeaveBalanceMax?: number;
  deductionPerDayMin?: number;
  deductionPerDayMax?: number;
  identityUserId?: string;
}

export interface EmployeeUpdateDto {
  employeeNumber?: string;
  dateOfJoining?: string;
  paidLeaveBalance: number;
  baseSalary: number;
  unpaidLeaveBalance: number;
  sickLeaveBalance: number;
  deductionPerDay: number;
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
  paidLeaveBalanceMin?: number;
  paidLeaveBalanceMax?: number;
  baseSalaryMin?: number;
  baseSalaryMax?: number;
  unpaidLeaveBalanceMin?: number;
  unpaidLeaveBalanceMax?: number;
  sickLeaveBalanceMin?: number;
  sickLeaveBalanceMax?: number;
  deductionPerDayMin?: number;
  deductionPerDayMax?: number;
  identityUserId?: string;
}
