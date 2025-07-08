import { Directive, OnInit, inject } from '@angular/core';

import { ListService, PermissionService, TrackByService } from '@abp/ng.core';

import { attendanceStatusOptions } from '../../../proxy/attendance-status.enum';
import type { AttendanceLogWithNavigationPropertiesDto } from '../../../proxy/attendance-logs/models';
import { AttendanceLogViewService } from '../services/attendance-log.service';
import { AttendanceLogDetailViewService } from '../services/attendance-log-detail.service';

export const ChildTabDependencies = [];

export const ChildComponentDependencies = [];

@Directive()
export abstract class AbstractAttendanceLogComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(AttendanceLogViewService);
  public readonly serviceDetail = inject(AttendanceLogDetailViewService);
  public readonly permissionService = inject(PermissionService);

  protected title = '::AttendanceLogs';
  protected isActionButtonVisible: boolean | null = null;

  attendanceStatusOptions = attendanceStatusOptions;

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

  update(record: AttendanceLogWithNavigationPropertiesDto) {
    this.serviceDetail.update(record);
  }

  delete(record: AttendanceLogWithNavigationPropertiesDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }

  checkActionButtonVisibility() {
    if (this.isActionButtonVisible !== null) {
      return;
    }

    const canEdit = this.permissionService.getGrantedPolicy('HRManagement.AttendanceLogs.Edit');
    const canDelete = this.permissionService.getGrantedPolicy('HRManagement.AttendanceLogs.Delete');
    this.isActionButtonVisible = canEdit || canDelete;
  }
}
