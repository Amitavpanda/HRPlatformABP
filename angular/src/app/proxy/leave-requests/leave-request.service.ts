import type {
  GetLeaveRequestsInput,
  LeaveRequestCreateDto,
  LeaveRequestDto,
  LeaveRequestExcelDownloadDto,
  LeaveRequestUpdateDto,
  LeaveRequestWithNavigationPropertiesDto,
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
export class LeaveRequestService {
  apiName = 'Default';

  create = (input: LeaveRequestCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LeaveRequestDto>(
      {
        method: 'POST',
        url: '/api/app/leave-requests',
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/leave-requests/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  deleteAll = (input: GetLeaveRequestsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/leave-requests/all',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          leaveRequestType: input.leaveRequestType,
          startDateMin: input.startDateMin,
          startDateMax: input.startDateMax,
          endDateMin: input.endDateMin,
          endDateMax: input.endDateMax,
          leaveRequestStatus: input.leaveRequestStatus,
          requestedOnMin: input.requestedOnMin,
          requestedOnMax: input.requestedOnMax,
          reviewedOnMin: input.reviewedOnMin,
          reviewedOnMax: input.reviewedOnMax,
          workflowInstanceId: input.workflowInstanceId,
          employeeId: input.employeeId,
          reviewedBy: input.reviewedBy,
        },
      },
      { apiName: this.apiName, ...config },
    );

  deleteByIds = (leaveRequestIds: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/leave-requests',
        params: { leaveRequestIds },
      },
      { apiName: this.apiName, ...config },
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LeaveRequestDto>(
      {
        method: 'GET',
        url: `/api/app/leave-requests/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/app/leave-requests/download-token',
      },
      { apiName: this.apiName, ...config },
    );

  getEmployeeLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>(
      {
        method: 'GET',
        url: '/api/app/leave-requests/employee-lookup',
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
        url: '/api/app/leave-requests/file',
        params: { downloadToken: input.downloadToken, fileId: input.fileId },
      },
      { apiName: this.apiName, ...config },
    );

  getIdentityUserLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>(
      {
        method: 'GET',
        url: '/api/app/leave-requests/identity-user-lookup',
        params: {
          filter: input.filter,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config },
    );

  getList = (input: GetLeaveRequestsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LeaveRequestWithNavigationPropertiesDto>>(
      {
        method: 'GET',
        url: '/api/app/leave-requests',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          leaveRequestType: input.leaveRequestType,
          startDateMin: input.startDateMin,
          startDateMax: input.startDateMax,
          endDateMin: input.endDateMin,
          endDateMax: input.endDateMax,
          leaveRequestStatus: input.leaveRequestStatus,
          requestedOnMin: input.requestedOnMin,
          requestedOnMax: input.requestedOnMax,
          reviewedOnMin: input.reviewedOnMin,
          reviewedOnMax: input.reviewedOnMax,
          workflowInstanceId: input.workflowInstanceId,
          employeeId: input.employeeId,
          reviewedBy: input.reviewedBy,
        },
      },
      { apiName: this.apiName, ...config },
    );

  getListAsExcelFile = (input: LeaveRequestExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/leave-requests/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          leaveRequestType: input.leaveRequestType,
          startDateMin: input.startDateMin,
          startDateMax: input.startDateMax,
          endDateMin: input.endDateMin,
          endDateMax: input.endDateMax,
          leaveRequestStatus: input.leaveRequestStatus,
          requestedOnMin: input.requestedOnMin,
          requestedOnMax: input.requestedOnMax,
          reviewedOnMin: input.reviewedOnMin,
          reviewedOnMax: input.reviewedOnMax,
          workflowInstanceId: input.workflowInstanceId,
          employeeId: input.employeeId,
          reviewedBy: input.reviewedBy,
        },
      },
      { apiName: this.apiName, ...config },
    );

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LeaveRequestWithNavigationPropertiesDto>(
      {
        method: 'GET',
        url: `/api/app/leave-requests/with-navigation-properties/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  update = (id: string, input: LeaveRequestUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LeaveRequestDto>(
      {
        method: 'PUT',
        url: `/api/app/leave-requests/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  uploadFile = (input: FormData, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AppFileDescriptorDto>(
      {
        method: 'POST',
        url: '/api/app/leave-requests/upload-file',
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  constructor(private restService: RestService) {}
}
