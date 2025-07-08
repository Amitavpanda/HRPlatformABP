import { inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService, TrackByService } from '@abp/ng.core';

import { finalize, tap } from 'rxjs/operators';

import { leaveRequestTypeOptions } from '../../../proxy/leave-request-type.enum';
import { leaveRequestStatusOptions } from '../../../proxy/leave-request-status.enum';
import type { LeaveRequestWithNavigationPropertiesDto } from '../../../proxy/leave-requests/models';
import { LeaveRequestService } from '../../../proxy/leave-requests/leave-request.service';

export abstract class AbstractLeaveRequestDetailViewService {
  protected readonly fb = inject(FormBuilder);
  protected readonly track = inject(TrackByService);

  public readonly proxyService = inject(LeaveRequestService);
  public readonly list = inject(ListService);

  public readonly getEmployeeLookup = this.proxyService.getEmployeeLookup;

  public readonly getIdentityUserLookup = this.proxyService.getIdentityUserLookup;

  leaveRequestTypeOptions = leaveRequestTypeOptions;
  leaveRequestStatusOptions = leaveRequestStatusOptions;

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  protected createRequest() {
    const formValues = {
      ...this.form.value,
    };

    if (this.selected) {
      return this.proxyService.update(this.selected.leaveRequest.id, {
        ...formValues,
        concurrencyStamp: this.selected.leaveRequest.concurrencyStamp,
      });
    }

    return this.proxyService.create(formValues);
  }

  buildForm() {
    const {
      leaveRequestType,
      startDate,
      endDate,
      leaveRequestStatus,
      requestedOn,
      reviewedOn,
      workflowInstanceId,
      employeeId,
      reviewedBy,
    } = this.selected?.leaveRequest || {};

    this.form = this.fb.group({
      leaveRequestType: [leaveRequestType ?? null, [Validators.required]],
      startDate: [startDate ?? null, [Validators.required]],
      endDate: [endDate ?? null, [Validators.required]],
      leaveRequestStatus: [leaveRequestStatus ?? null, [Validators.required]],
      requestedOn: [requestedOn ?? null, [Validators.required]],
      reviewedOn: [reviewedOn ?? null, []],
      workflowInstanceId: [
        workflowInstanceId ?? null,
        [Validators.minLength(0), Validators.maxLength(1000)],
      ],
      employeeId: [employeeId ?? null, [Validators.required]],
      reviewedBy: [reviewedBy ?? null, []],
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

  update(record: LeaveRequestWithNavigationPropertiesDto) {
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
