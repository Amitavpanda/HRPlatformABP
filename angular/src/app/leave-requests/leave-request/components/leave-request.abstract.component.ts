import { Directive, OnInit, inject } from '@angular/core';

import { ListService, PermissionService, TrackByService } from '@abp/ng.core';

import { leaveRequestTypeOptions } from '../../../proxy/leave-request-type.enum';
import { leaveRequestStatusOptions } from '../../../proxy/leave-request-status.enum';
import type { LeaveRequestWithNavigationPropertiesDto } from '../../../proxy/leave-requests/models';
import { LeaveRequestViewService } from '../services/leave-request.service';
import { LeaveRequestDetailViewService } from '../services/leave-request-detail.service';

export const ChildTabDependencies = [];

export const ChildComponentDependencies = [];

@Directive()
export abstract class AbstractLeaveRequestComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(LeaveRequestViewService);
  public readonly serviceDetail = inject(LeaveRequestDetailViewService);
  public readonly permissionService = inject(PermissionService);

  protected title = '::LeaveRequests';
  protected isActionButtonVisible: boolean | null = null;

  leaveRequestTypeOptions = leaveRequestTypeOptions;
  leaveRequestStatusOptions = leaveRequestStatusOptions;

  ngOnInit() {
    this.service.hookToQuery();
    this.checkActionButtonVisibility();
  }

  clearFilters() {
    this.service.clearFilters();
  }

  showForm() {
    this.serviceDetail.showForm();
  }

  create() {
    this.serviceDetail.selected = undefined;
    this.serviceDetail.showForm();
  }

  update(record: LeaveRequestWithNavigationPropertiesDto) {
    this.serviceDetail.update(record);
  }

  delete(record: LeaveRequestWithNavigationPropertiesDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }

  checkActionButtonVisibility() {
    if (this.isActionButtonVisible !== null) {
      return;
    }

    const canEdit = this.permissionService.getGrantedPolicy('HRManagement.LeaveRequests.Edit');
    const canDelete = this.permissionService.getGrantedPolicy('HRManagement.LeaveRequests.Delete');
    this.isActionButtonVisible = canEdit || canDelete;
  }
}
