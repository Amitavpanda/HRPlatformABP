import type {
  AttendanceLogCreateDto,
  AttendanceLogDto,
  AttendanceLogExcelDownloadDto,
  AttendanceLogUpdateDto,
  AttendanceLogWithNavigationPropertiesDto,
  GetAttendanceLogsInput,
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
export class AttendanceLogService {
  apiName = 'Default';

  create = (input: AttendanceLogCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AttendanceLogDto>(
      {
        method: 'POST',
        url: '/api/app/attendance-logs',
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/attendance-logs/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  deleteAll = (input: GetAttendanceLogsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/attendance-logs/all',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          dateMin: input.dateMin,
          dateMax: input.dateMax,
          checkInTimeMin: input.checkInTimeMin,
          checkInTimeMax: input.checkInTimeMax,
          checkOutTimeMin: input.checkOutTimeMin,
          checkOutTimeMax: input.checkOutTimeMax,
          status: input.status,
          employeeId: input.employeeId,
        },
      },
      { apiName: this.apiName, ...config },
    );

  deleteByIds = (attendanceLogIds: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/attendance-logs',
        params: { attendanceLogIds },
      },
      { apiName: this.apiName, ...config },
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AttendanceLogDto>(
      {
        method: 'GET',
        url: `/api/app/attendance-logs/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/app/attendance-logs/download-token',
      },
      { apiName: this.apiName, ...config },
    );

  getEmployeeLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>(
      {
        method: 'GET',
        url: '/api/app/attendance-logs/employee-lookup',
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
        url: '/api/app/attendance-logs/file',
        params: { downloadToken: input.downloadToken, fileId: input.fileId },
      },
      { apiName: this.apiName, ...config },
    );

  getList = (input: GetAttendanceLogsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<AttendanceLogWithNavigationPropertiesDto>>(
      {
        method: 'GET',
        url: '/api/app/attendance-logs',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          dateMin: input.dateMin,
          dateMax: input.dateMax,
          checkInTimeMin: input.checkInTimeMin,
          checkInTimeMax: input.checkInTimeMax,
          checkOutTimeMin: input.checkOutTimeMin,
          checkOutTimeMax: input.checkOutTimeMax,
          status: input.status,
          employeeId: input.employeeId,
        },
      },
      { apiName: this.apiName, ...config },
    );

  getListAsExcelFile = (input: AttendanceLogExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/attendance-logs/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          dateMin: input.dateMin,
          dateMax: input.dateMax,
          checkInTimeMin: input.checkInTimeMin,
          checkInTimeMax: input.checkInTimeMax,
          checkOutTimeMin: input.checkOutTimeMin,
          checkOutTimeMax: input.checkOutTimeMax,
          status: input.status,
          employeeId: input.employeeId,
        },
      },
      { apiName: this.apiName, ...config },
    );

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AttendanceLogWithNavigationPropertiesDto>(
      {
        method: 'GET',
        url: `/api/app/attendance-logs/with-navigation-properties/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  update = (id: string, input: AttendanceLogUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AttendanceLogDto>(
      {
        method: 'PUT',
        url: `/api/app/attendance-logs/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  uploadFile = (input: FormData, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AppFileDescriptorDto>(
      {
        method: 'POST',
        url: '/api/app/attendance-logs/upload-file',
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  constructor(private restService: RestService) {}
}
