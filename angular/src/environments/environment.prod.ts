import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44325/',
  redirectUri: baseUrl,
  clientId: 'HRManagement_App',
  responseType: 'code',
  scope: 'offline_access HRManagement',
  requireHttps: true,
  impersonation: {
    userImpersonation: true,
    tenantImpersonation: true,
  },
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'HRManagement',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44325',
      rootNamespace: 'HRManagement',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
