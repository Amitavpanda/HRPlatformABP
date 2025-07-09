import type {
  GetPayrollRecordsInput,
  PayrollRecordCreateDto,
  PayrollRecordDto,
  PayrollRecordExcelDownloadDto,
  PayrollRecordUpdateDto,
  PayrollRecordWithNavigationPropertiesDto,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type {
  AppFileDescriptorDto,
  DownloadTokenResultDto,
  GetFileInput,
  LookupDto,
  LookupRequestDto,
} from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class PayrollRecordService {
  apiName = 'Default';

  create = (input: PayrollRecordCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PayrollRecordDto>(
      {
        method: 'POST',
        url: '/api/app/payroll-records',
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/payroll-records/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  deleteAll = (input: GetPayrollRecordsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/payroll-records/all',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          monthMin: input.monthMin,
          monthMax: input.monthMax,
          yearMin: input.yearMin,
          yearMax: input.yearMax,
          baseSalaryMin: input.baseSalaryMin,
          baseSalaryMax: input.baseSalaryMax,
          leaveDeductionsMin: input.leaveDeductionsMin,
          leaveDeductionsMax: input.leaveDeductionsMax,
          netPayMin: input.netPayMin,
          netPayMax: input.netPayMax,
          status: input.status,
          payslipUrl: input.payslipUrl,
          employeeId: input.employeeId,
        },
      },
      { apiName: this.apiName, ...config },
    );

  deleteByIds = (payrollRecordIds: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/payroll-records',
        params: { payrollRecordIds },
      },
      { apiName: this.apiName, ...config },
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PayrollRecordDto>(
      {
        method: 'GET',
        url: `/api/app/payroll-records/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/app/payroll-records/download-token',
      },
      { apiName: this.apiName, ...config },
    );

  getEmployeeLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>(
      {
        method: 'GET',
        url: '/api/app/payroll-records/employee-lookup',
        params: {
          filter: input.filter,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config },
    );

  getFile = (input: GetFileInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/payroll-records/file',
        params: { downloadToken: input.downloadToken, fileId: input.fileId },
      },
      { apiName: this.apiName, ...config },
    );

  getList = (input: GetPayrollRecordsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<PayrollRecordWithNavigationPropertiesDto>>(
      {
        method: 'GET',
        url: '/api/app/payroll-records',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          monthMin: input.monthMin,
          monthMax: input.monthMax,
          yearMin: input.yearMin,
          yearMax: input.yearMax,
          baseSalaryMin: input.baseSalaryMin,
          baseSalaryMax: input.baseSalaryMax,
          leaveDeductionsMin: input.leaveDeductionsMin,
          leaveDeductionsMax: input.leaveDeductionsMax,
          netPayMin: input.netPayMin,
          netPayMax: input.netPayMax,
          status: input.status,
          payslipUrl: input.payslipUrl,
          employeeId: input.employeeId,
        },
      },
      { apiName: this.apiName, ...config },
    );

  getListAsExcelFile = (input: PayrollRecordExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/payroll-records/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          monthMin: input.monthMin,
          monthMax: input.monthMax,
          yearMin: input.yearMin,
          yearMax: input.yearMax,
          baseSalaryMin: input.baseSalaryMin,
          baseSalaryMax: input.baseSalaryMax,
          leaveDeductionsMin: input.leaveDeductionsMin,
          leaveDeductionsMax: input.leaveDeductionsMax,
          netPayMin: input.netPayMin,
          netPayMax: input.netPayMax,
          status: input.status,
          payslipUrl: input.payslipUrl,
          employeeId: input.employeeId,
        },
      },
      { apiName: this.apiName, ...config },
    );

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PayrollRecordWithNavigationPropertiesDto>(
      {
        method: 'GET',
        url: `/api/app/payroll-records/with-navigation-properties/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  update = (id: string, input: PayrollRecordUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PayrollRecordDto>(
      {
        method: 'PUT',
        url: `/api/app/payroll-records/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  uploadFile = (input: FormData, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AppFileDescriptorDto>(
      {
        method: 'POST',
        url: '/api/app/payroll-records/upload-file',
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  constructor(private restService: RestService) {}
}
