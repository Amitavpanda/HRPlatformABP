import { Directive, OnInit, inject } from '@angular/core';

import { ListService, PermissionService, TrackByService } from '@abp/ng.core';

import type { HRManagerWithNavigationPropertiesDto } from '../../../proxy/hrmanagers/models';
import { HRManagerViewService } from '../services/hrmanager.service';
import { HRManagerDetailViewService } from '../services/hrmanager-detail.service';

export const ChildTabDependencies = [];

export const ChildComponentDependencies = [];

@Directive()
export abstract class AbstractHRManagerComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(HRManagerViewService);
  public readonly serviceDetail = inject(HRManagerDetailViewService);
  public readonly permissionService = inject(PermissionService);

  protected title = '::HRManagers';
  protected isActionButtonVisible: boolean | null = null;

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

  update(record: HRManagerWithNavigationPropertiesDto) {
    this.serviceDetail.update(record);
  }

  delete(record: HRManagerWithNavigationPropertiesDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }

  checkActionButtonVisibility() {
    if (this.isActionButtonVisible !== null) {
      return;
    }

    const canEdit = this.permissionService.getGrantedPolicy('HRManagement.HRManagers.Edit');
    const canDelete = this.permissionService.getGrantedPolicy('HRManagement.HRManagers.Delete');
    this.isActionButtonVisible = canEdit || canDelete;
  }
}
