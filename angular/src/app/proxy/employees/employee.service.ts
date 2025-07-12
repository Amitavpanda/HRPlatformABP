import type {
  EmployeeCreateDto,
  EmployeeDto,
  EmployeeExcelDownloadDto,
  EmployeeUpdateDto,
  EmployeeWithNavigationPropertiesDto,
  GetEmployeesInput,
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
export class EmployeeService {
  apiName = 'Default';

  create = (input: EmployeeCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EmployeeDto>(
      {
        method: 'POST',
        url: '/api/app/employees',
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/employees/${id}`,
      },
      { apiName: this.apiName, ...config },
    );


  getEmployeeByUserId = (userId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EmployeeDto>(
      {
        method: 'GET',
        url: '/api/app/employees',
        params: {
          identityUserId: userId,
        },
      },
      { apiName: this.apiName, ...config },
    );

  deleteAll = (input: GetEmployeesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/employees/all',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          employeeNumber: input.employeeNumber,
          dateOfJoiningMin: input.dateOfJoiningMin,
          dateOfJoiningMax: input.dateOfJoiningMax,
          paidLeaveBalanceMin: input.paidLeaveBalanceMin,
          paidLeaveBalanceMax: input.paidLeaveBalanceMax,
          baseSalaryMin: input.baseSalaryMin,
          baseSalaryMax: input.baseSalaryMax,
          unpaidLeaveBalanceMin: input.unpaidLeaveBalanceMin,
          unpaidLeaveBalanceMax: input.unpaidLeaveBalanceMax,
          sickLeaveBalanceMin: input.sickLeaveBalanceMin,
          sickLeaveBalanceMax: input.sickLeaveBalanceMax,
          deductionPerDayMin: input.deductionPerDayMin,
          deductionPerDayMax: input.deductionPerDayMax,
          identityUserId: input.identityUserId,
        },
      },
      { apiName: this.apiName, ...config },
    );

  deleteByIds = (employeeIds: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/employees',
        params: { employeeIds },
      },
      { apiName: this.apiName, ...config },
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EmployeeDto>(
      {
        method: 'GET',
        url: `/api/app/employees/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/app/employees/download-token',
      },
      { apiName: this.apiName, ...config },
    );

  getFile = (input: GetFileInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/employees/file',
        params: { downloadToken: input.downloadToken, fileId: input.fileId },
      },
      { apiName: this.apiName, ...config },
    );

  getIdentityUserLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>(
      {
        method: 'GET',
        url: '/api/app/employees/identity-user-lookup',
        params: {
          filter: input.filter,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config },
    );

  getList = (input: GetEmployeesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<EmployeeWithNavigationPropertiesDto>>(
      {
        method: 'GET',
        url: '/api/app/employees',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          employeeNumber: input.employeeNumber,
          dateOfJoiningMin: input.dateOfJoiningMin,
          dateOfJoiningMax: input.dateOfJoiningMax,
          paidLeaveBalanceMin: input.paidLeaveBalanceMin,
          paidLeaveBalanceMax: input.paidLeaveBalanceMax,
          baseSalaryMin: input.baseSalaryMin,
          baseSalaryMax: input.baseSalaryMax,
          unpaidLeaveBalanceMin: input.unpaidLeaveBalanceMin,
          unpaidLeaveBalanceMax: input.unpaidLeaveBalanceMax,
          sickLeaveBalanceMin: input.sickLeaveBalanceMin,
          sickLeaveBalanceMax: input.sickLeaveBalanceMax,
          deductionPerDayMin: input.deductionPerDayMin,
          deductionPerDayMax: input.deductionPerDayMax,
          identityUserId: input.identityUserId,
        },
      },
      { apiName: this.apiName, ...config },
    );

  getListAsExcelFile = (input: EmployeeExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/employees/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          employeeNumber: input.employeeNumber,
          dateOfJoiningMin: input.dateOfJoiningMin,
          dateOfJoiningMax: input.dateOfJoiningMax,
          paidLeaveBalanceMin: input.paidLeaveBalanceMin,
          paidLeaveBalanceMax: input.paidLeaveBalanceMax,
          baseSalaryMin: input.baseSalaryMin,
          baseSalaryMax: input.baseSalaryMax,
          unpaidLeaveBalanceMin: input.unpaidLeaveBalanceMin,
          unpaidLeaveBalanceMax: input.unpaidLeaveBalanceMax,
          sickLeaveBalanceMin: input.sickLeaveBalanceMin,
          sickLeaveBalanceMax: input.sickLeaveBalanceMax,
          deductionPerDayMin: input.deductionPerDayMin,
          deductionPerDayMax: input.deductionPerDayMax,
          identityUserId: input.identityUserId,
        },
      },
      { apiName: this.apiName, ...config },
    );

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EmployeeWithNavigationPropertiesDto>(
      {
        method: 'GET',
        url: `/api/app/employees/with-navigation-properties/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  update = (id: string, input: EmployeeUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EmployeeDto>(
      {
        method: 'PUT',
        url: `/api/app/employees/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  uploadFile = (input: FormData, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AppFileDescriptorDto>(
      {
        method: 'POST',
        url: '/api/app/employees/upload-file',
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  constructor(private restService: RestService) {}
}
