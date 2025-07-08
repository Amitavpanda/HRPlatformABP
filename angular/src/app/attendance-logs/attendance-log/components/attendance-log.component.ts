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
import { AttendanceLogViewService } from '../services/attendance-log.service';
import { AttendanceLogDetailViewService } from '../services/attendance-log-detail.service';
import { AttendanceLogDetailModalComponent } from './attendance-log-detail.component';
import {
  AbstractAttendanceLogComponent,
  ChildTabDependencies,
  ChildComponentDependencies,
} from './attendance-log.abstract.component';

@Component({
  selector: 'app-attendance-log',
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
    AttendanceLogDetailModalComponent,
    ...ChildComponentDependencies,
  ],
  providers: [
    ListService,
    AttendanceLogViewService,
    AttendanceLogDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
    { provide: NgbTimeAdapter, useClass: TimeAdapter },
  ],
  templateUrl: './attendance-log.component.html',
  styles: `
    ::ng-deep.datatable-row-detail {
      background: transparent !important;
    }
  `,
})
export class AttendanceLogComponent extends AbstractAttendanceLogComponent {}
