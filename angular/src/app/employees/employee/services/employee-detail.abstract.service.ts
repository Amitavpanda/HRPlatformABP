import { inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService, TrackByService } from '@abp/ng.core';

import { finalize, tap } from 'rxjs/operators';

import type { EmployeeWithNavigationPropertiesDto } from '../../../proxy/employees/models';
import { EmployeeService } from '../../../proxy/employees/employee.service';

export abstract class AbstractEmployeeDetailViewService {
  protected readonly fb = inject(FormBuilder);
  protected readonly track = inject(TrackByService);

  public readonly proxyService = inject(EmployeeService);
  public readonly list = inject(ListService);

  public readonly getIdentityUserLookup = this.proxyService.getIdentityUserLookup;

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  protected createRequest() {
    const formValues = {
      ...this.form.value,
    };

    if (this.selected) {
      return this.proxyService.update(this.selected.employee.id, {
        ...formValues,
        concurrencyStamp: this.selected.employee.concurrencyStamp,
      });
    }

    return this.proxyService.create(formValues);
  }

  buildForm() {
    const {
      employeeNumber,
      dateOfJoining,
      paidLeaveBalance,
      baseSalary,
      unpaidLeaveBalance,
      sickLeaveBalance,
      deductionPerDay,
      identityUserId,
    } = this.selected?.employee || {};

    this.form = this.fb.group({
      employeeNumber: [
        employeeNumber ?? null,
        [Validators.minLength(0), Validators.maxLength(100)],
      ],
      dateOfJoining: [dateOfJoining ?? null, [Validators.required]],
      paidLeaveBalance: [
        paidLeaveBalance ?? null,
        [Validators.required, Validators.min(0), Validators.max(1000)],
      ],
      baseSalary: [baseSalary ?? null, [Validators.required]],
      unpaidLeaveBalance: [
        unpaidLeaveBalance ?? null,
        [Validators.required, Validators.min(0), Validators.max(1000)],
      ],
      sickLeaveBalance: [
        sickLeaveBalance ?? null,
        [Validators.required, Validators.min(0), Validators.max(1000)],
      ],
      deductionPerDay: [
        deductionPerDay ?? null,
        [Validators.required, Validators.min(0), Validators.max(1000)],
      ],
      identityUserId: [identityUserId ?? null, []],
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

  update(record: EmployeeWithNavigationPropertiesDto) {
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
