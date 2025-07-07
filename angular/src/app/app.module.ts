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
import { ThemeSharedModule, withHttpErrorConfig, withValidationBluePrint, provideAbpThemeShared } from '@abp/ng.theme.shared';
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
    ThemeLeptonXModule.forRoot(), SideMenuLayoutModule.forRoot(), ThemeSharedModule
],
    providers: [APP_ROUTE_PROVIDER, provideAbpThemeShared(withValidationBluePrint({
        wrongPassword: 'Please choose 1q2w3E*'
    }), withHttpErrorConfig({
        errorScreen: {
            component: HttpErrorComponent,
            forWhichErrors: [401, 403, 404, 500],
            hideCloseIcon: true
        }
    }))],
    bootstrap: [AppComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {
}
