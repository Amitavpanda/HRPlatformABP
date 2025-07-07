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
import { HRManagerViewService } from '../services/hrmanager.service';
import { HRManagerDetailViewService } from '../services/hrmanager-detail.service';
import { HRManagerDetailModalComponent } from './hrmanager-detail.component';
import {
  AbstractHRManagerComponent,
  ChildTabDependencies,
  ChildComponentDependencies,
} from './hrmanager.abstract.component';

@Component({
  selector: 'app-hrmanager',
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
    HRManagerDetailModalComponent,
    ...ChildComponentDependencies,
  ],
  providers: [
    ListService,
    HRManagerViewService,
    HRManagerDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
    { provide: NgbTimeAdapter, useClass: TimeAdapter },
  ],
  templateUrl: './hrmanager.component.html',
  styles: `
    ::ng-deep.datatable-row-detail {
      background: transparent !important;
    }
  `,
})
export class HRManagerComponent extends AbstractHRManagerComponent {}
