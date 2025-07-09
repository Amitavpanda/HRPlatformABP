import { CoreModule } from '@abp/ng.core';
import { GdprConfigModule } from '@volo/abp.ng.gdpr/config';
import { SettingManagementConfigModule } from '@abp/ng.setting-management/config';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommercialUiConfigModule } from '@volo/abp.commercial.ng.ui/config';
import { AccountAdminConfigModule } from '@volo/abp.ng.account/admin/config';
import { AccountPublicConfigModule } from '@volo/abp.ng.account/public/config';
import { AuditLoggingConfigModule } from '@volo/abp.ng.audit-logging/config';
import { IdentityConfigModule } from '@volo/abp.ng.identity/config';
import { LanguageManagementConfigModule } from '@volo/abp.ng.language-management/config';
import { registerLocale } from '@volo/abp.ng.language-management/locale';
import { SaasConfigModule } from '@volo/abp.ng.saas/config';
import { TextTemplateManagementConfigModule } from '@volo/abp.ng.text-template-management/config';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_ROUTE_PROVIDER } from './route.provider';
import { OpeniddictproConfigModule } from '@volo/abp.ng.openiddictpro/config';
import { FeatureManagementModule } from '@abp/ng.feature-management';
import { AbpOAuthModule } from '@abp/ng.oauth';
import { ThemeLeptonXModule, HttpErrorComponent } from '@volosoft/abp.ng.theme.lepton-x';
import { SideMenuLayoutModule } from '@volosoft/abp.ng.theme.lepton-x/layouts';
import {
  ThemeSharedModule,
  withHttpErrorConfig,
  withValidationBluePrint,
  provideAbpThemeShared,
} from '@abp/ng.theme.shared';
import { HRMANAGERS_HRMANAGER_ROUTE_PROVIDER } from './hrmanagers/hrmanager/providers/hrmanager-route.provider';
import { EMPLOYEES_EMPLOYEE_ROUTE_PROVIDER } from './employees/employee/providers/employee-route.provider';
import { ATTENDANCE_LOGS_ATTENDANCE_LOG_ROUTE_PROVIDER } from './attendance-logs/attendance-log/providers/attendance-log-route.provider';
import { LEAVE_REQUESTS_LEAVE_REQUEST_ROUTE_PROVIDER } from './leave-requests/leave-request/providers/leave-request-route.provider';
import { PAYROLL_RECORDS_PAYROLL_RECORD_ROUTE_PROVIDER } from './payroll-records/payroll-record/providers/payroll-record-route.provider';
@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule.forRoot({
      environment,
      registerLocaleFn: registerLocale(),
    }),
    AbpOAuthModule.forRoot(),
    AccountAdminConfigModule.forRoot(),
    AccountPublicConfigModule.forRoot(),
    IdentityConfigModule.forRoot(),
    LanguageManagementConfigModule.forRoot(),
    SaasConfigModule.forRoot(),
    AuditLoggingConfigModule.forRoot(),
    OpeniddictproConfigModule.forRoot(),
    TextTemplateManagementConfigModule.forRoot(),
    SettingManagementConfigModule.forRoot(),
    CommercialUiConfigModule.forRoot(),
    FeatureManagementModule.forRoot(),
    GdprConfigModule.forRoot({
      privacyPolicyUrl: 'gdpr-cookie-consent/privacy',
      cookiePolicyUrl: 'gdpr-cookie-consent/cookie',
    }),
    ThemeLeptonXModule.forRoot(),
    SideMenuLayoutModule.forRoot(),
    ThemeSharedModule,
  ],
  providers: [
    APP_ROUTE_PROVIDER,
    provideAbpThemeShared(
      withValidationBluePrint({
        wrongPassword: 'Please choose 1q2w3E*',
      }),
      withHttpErrorConfig({
        errorScreen: {
          component: HttpErrorComponent,
          forWhichErrors: [401, 403, 404, 500],
          hideCloseIcon: true,
        },
      }),
    ),
    HRMANAGERS_HRMANAGER_ROUTE_PROVIDER,
    EMPLOYEES_EMPLOYEE_ROUTE_PROVIDER,
    ATTENDANCE_LOGS_ATTENDANCE_LOG_ROUTE_PROVIDER,
    LEAVE_REQUESTS_LEAVE_REQUEST_ROUTE_PROVIDER,
    PAYROLL_RECORDS_PAYROLL_RECORD_ROUTE_PROVIDER,
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}
