import { Directive, OnInit, inject } from '@angular/core';

import { ListService, PermissionService, TrackByService } from '@abp/ng.core';

import type { EmployeeWithNavigationPropertiesDto } from '../../../proxy/employees/models';
import { EmployeeViewService } from '../services/employee.service';
import { EmployeeDetailViewService } from '../services/employee-detail.service';

export const ChildTabDependencies = [];

export const ChildComponentDependencies = [];

@Directive()
export abstract class AbstractEmployeeComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(EmployeeViewService);
  public readonly serviceDetail = inject(EmployeeDetailViewService);
  public readonly permissionService = inject(PermissionService);

  protected title = '::Employees';
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

  update(record: EmployeeWithNavigationPropertiesDto) {
    this.serviceDetail.update(record);
  }

  delete(record: EmployeeWithNavigationPropertiesDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }

  checkActionButtonVisibility() {
    if (this.isActionButtonVisible !== null) {
      return;
    }

    const canEdit = this.permissionService.getGrantedPolicy('HRManagement.Employees.Edit');
    const canDelete = this.permissionService.getGrantedPolicy('HRManagement.Employees.Delete');
    this.isActionButtonVisible = canEdit || canDelete;
  }
}
