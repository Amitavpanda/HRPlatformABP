import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
  NgbDateAdapter,
  NgbTimeAdapter,
  NgbCollapseModule,
  NgbDatepickerModule,
  NgbTimepickerModule,
  NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { ListService, CoreModule } from '@abp/ng.core';
import { ThemeSharedModule, DateAdapter, TimeAdapter } from '@abp/ng.theme.shared';
import { PageModule } from '@abp/ng.components/page';
import { CommercialUiModule } from '@volo/abp.commercial.ng.ui';
import { PayrollRecordViewService } from '../services/payroll-record.service';
import { PayrollRecordDetailViewService } from '../services/payroll-record-detail.service';
import { PayrollRecordDetailModalComponent } from './payroll-record-detail.component';
import {
  AbstractPayrollRecordComponent,
  ChildTabDependencies,
  ChildComponentDependencies,
} from './payroll-record.abstract.component';

@Component({
  selector: 'app-payroll-record',
  changeDetection: ChangeDetectionStrategy.Default,
  imports: [
    ...ChildTabDependencies,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbTimepickerModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    PageModule,
    CoreModule,
    ThemeSharedModule,
    CommercialUiModule,
    PayrollRecordDetailModalComponent,
    ...ChildComponentDependencies,
  ],
  providers: [
    ListService,
    PayrollRecordViewService,
    PayrollRecordDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
    { provide: NgbTimeAdapter, useClass: TimeAdapter },
  ],
  templateUrl: './payroll-record.component.html',
  styles: `
    ::ng-deep.datatable-row-detail {
      background: transparent !important;
    }
  `,
})
export class PayrollRecordComponent extends AbstractPayrollRecordComponent {}
