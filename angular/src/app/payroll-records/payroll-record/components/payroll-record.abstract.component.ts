import { Directive, OnInit, inject } from '@angular/core';

import { ListService, PermissionService, TrackByService } from '@abp/ng.core';

import { payrollRecordStatusOptions } from '../../../proxy/payroll-record-status.enum';
import type { PayrollRecordWithNavigationPropertiesDto } from '../../../proxy/payroll-records/models';
import { PayrollRecordViewService } from '../services/payroll-record.service';
import { PayrollRecordDetailViewService } from '../services/payroll-record-detail.service';

export const ChildTabDependencies = [];

export const ChildComponentDependencies = [];

@Directive()
export abstract class AbstractPayrollRecordComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(PayrollRecordViewService);
  public readonly serviceDetail = inject(PayrollRecordDetailViewService);
  public readonly permissionService = inject(PermissionService);

  protected title = '::PayrollRecords';
  protected isActionButtonVisible: boolean | null = null;

  payrollRecordStatusOptions = payrollRecordStatusOptions;

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

  update(record: PayrollRecordWithNavigationPropertiesDto) {
    this.serviceDetail.update(record);
  }

  delete(record: PayrollRecordWithNavigationPropertiesDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }

  checkActionButtonVisibility() {
    if (this.isActionButtonVisible !== null) {
      return;
    }

    const canEdit = this.permissionService.getGrantedPolicy('HRManagement.PayrollRecords.Edit');
    const canDelete = this.permissionService.getGrantedPolicy('HRManagement.PayrollRecords.Delete');
    this.isActionButtonVisible = canEdit || canDelete;
  }
}
