import { inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService, TrackByService } from '@abp/ng.core';

import { finalize, tap } from 'rxjs/operators';

import { payrollRecordStatusOptions } from '../../../proxy/payroll-record-status.enum';
import type { PayrollRecordWithNavigationPropertiesDto } from '../../../proxy/payroll-records/models';
import { PayrollRecordService } from '../../../proxy/payroll-records/payroll-record.service';

export abstract class AbstractPayrollRecordDetailViewService {
  protected readonly fb = inject(FormBuilder);
  protected readonly track = inject(TrackByService);

  public readonly proxyService = inject(PayrollRecordService);
  public readonly list = inject(ListService);

  public readonly getEmployeeLookup = this.proxyService.getEmployeeLookup;

  payrollRecordStatusOptions = payrollRecordStatusOptions;

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  protected createRequest() {
    const formValues = {
      ...this.form.value,
    };

    if (this.selected) {
      return this.proxyService.update(this.selected.payrollRecord.id, {
        ...formValues,
        concurrencyStamp: this.selected.payrollRecord.concurrencyStamp,
      });
    }

    return this.proxyService.create(formValues);
  }

  buildForm() {
    const { month, year, baseSalary, leaveDeductions, netPay, status, payslipUrl, employeeId } =
      this.selected?.payrollRecord || {};

    this.form = this.fb.group({
      month: [month ?? null, [Validators.required, Validators.min(0), Validators.max(13)]],
      year: [year ?? null, [Validators.required, Validators.min(0), Validators.max(10000)]],
      baseSalary: [baseSalary ?? null, [Validators.required]],
      leaveDeductions: [leaveDeductions ?? null, [Validators.required]],
      netPay: [netPay ?? null, [Validators.required]],
      status: [status ?? null, [Validators.required]],
      payslipUrl: [payslipUrl ?? null, []],
      employeeId: [employeeId ?? null, [Validators.required]],
    });
  }

  showForm() {
    this.buildForm();
    this.isVisible = true;
  }

  create() {
    this.selected = undefined;
    this.showForm();
  }

  update(record: PayrollRecordWithNavigationPropertiesDto) {
    this.selected = record;
    this.showForm();
  }

  hideForm() {
    this.isVisible = false;
  }

  submitForm() {
    if (this.form.invalid) return;

    this.isBusy = true;

    const request = this.createRequest().pipe(
      finalize(() => (this.isBusy = false)),
      tap(() => this.hideForm()),
    );

    request.subscribe(this.list.get);
  }

  changeVisible($event: boolean) {
    this.isVisible = $event;
  }
}
