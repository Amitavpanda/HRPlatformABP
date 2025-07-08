import { inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService, TrackByService } from '@abp/ng.core';

import { finalize, tap } from 'rxjs/operators';

import { attendanceStatusOptions } from '../../../proxy/attendance-status.enum';
import type { AttendanceLogWithNavigationPropertiesDto } from '../../../proxy/attendance-logs/models';
import { AttendanceLogService } from '../../../proxy/attendance-logs/attendance-log.service';

export abstract class AbstractAttendanceLogDetailViewService {
  protected readonly fb = inject(FormBuilder);
  protected readonly track = inject(TrackByService);

  public readonly proxyService = inject(AttendanceLogService);
  public readonly list = inject(ListService);

  public readonly getEmployeeLookup = this.proxyService.getEmployeeLookup;

  attendanceStatusOptions = attendanceStatusOptions;

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  protected createRequest() {
    const formValues = {
      ...this.form.value,
    };

    if (this.selected) {
      return this.proxyService.update(this.selected.attendanceLog.id, {
        ...formValues,
        concurrencyStamp: this.selected.attendanceLog.concurrencyStamp,
      });
    }

    return this.proxyService.create(formValues);
  }

  buildForm() {
    const { date, checkInTime, checkOutTime, status, employeeId } =
      this.selected?.attendanceLog || {};

    this.form = this.fb.group({
      date: [date ?? null, [Validators.required]],
      checkInTime: [checkInTime ?? null, [Validators.required]],
      checkOutTime: [checkOutTime ?? null, [Validators.required]],
      status: [status ?? null, [Validators.required]],
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

  update(record: AttendanceLogWithNavigationPropertiesDto) {
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
