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
import { LeaveRequestViewService } from '../services/leave-request.service';
import { LeaveRequestDetailViewService } from '../services/leave-request-detail.service';
import { LeaveRequestDetailModalComponent } from './leave-request-detail.component';
import {
  AbstractLeaveRequestComponent,
  ChildTabDependencies,
  ChildComponentDependencies,
} from './leave-request.abstract.component';

@Component({
  selector: 'app-leave-request',
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
    LeaveRequestDetailModalComponent,
    ...ChildComponentDependencies,
  ],
  providers: [
    ListService,
    LeaveRequestViewService,
    LeaveRequestDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
    { provide: NgbTimeAdapter, useClass: TimeAdapter },
  ],
  templateUrl: './leave-request.component.html',
  styles: `
    ::ng-deep.datatable-row-detail {
      background: transparent !important;
    }
  `,
})
export class LeaveRequestComponent extends AbstractLeaveRequestComponent {}
