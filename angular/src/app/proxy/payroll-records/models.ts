import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { PayrollRecordStatus } from '../payroll-record-status.enum';
import type { EmployeeDto } from '../employees/models';

export interface GetPayrollRecordsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  monthMin?: number;
  monthMax?: number;
  yearMin?: number;
  yearMax?: number;
  baseSalaryMin?: number;
  baseSalaryMax?: number;
  leaveDeductionsMin?: number;
  leaveDeductionsMax?: number;
  netPayMin?: number;
  netPayMax?: number;
  status?: PayrollRecordStatus;
  payslipUrl?: string;
  employeeId?: string;
}

export interface PayrollRecordCreateDto {
  month: number;
  year: number;
  baseSalary: number;
  leaveDeductions: number;
  netPay: number;
  status: PayrollRecordStatus;
  payslipUrl?: string;
  employeeId: string;
}

export interface PayrollRecordDto extends FullAuditedEntityDto<string> {
  month: number;
  year: number;
  baseSalary: number;
  leaveDeductions: number;
  netPay: number;
  status: PayrollRecordStatus;
  payslipUrl?: string;
  employeeId: string;
  concurrencyStamp?: string;
}

export interface PayrollRecordExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  monthMin?: number;
  monthMax?: number;
  yearMin?: number;
  yearMax?: number;
  baseSalaryMin?: number;
  baseSalaryMax?: number;
  leaveDeductionsMin?: number;
  leaveDeductionsMax?: number;
  netPayMin?: number;
  netPayMax?: number;
  status?: PayrollRecordStatus;
  payslipUrl?: string;
  employeeId?: string;
}

export interface PayrollRecordUpdateDto {
  month: number;
  year: number;
  baseSalary: number;
  leaveDeductions: number;
  netPay: number;
  status: PayrollRecordStatus;
  payslipUrl?: string;
  employeeId: string;
  concurrencyStamp?: string;
}

export interface PayrollRecordWithNavigationPropertiesDto {
  payrollRecord: PayrollRecordDto;
  employee: EmployeeDto;
}
