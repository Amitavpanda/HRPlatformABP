import type { ExtensibleEntityDto } from '@abp/ng.core';

export interface IdentityUserDto extends ExtensibleEntityDto<string> {
  tenantId?: string;
  userName?: string;
  email?: string;
  name?: string;
  surname?: string;
  emailConfirmed: boolean;
  phoneNumber?: string;
  phoneNumberConfirmed: boolean;
  supportTwoFactor: boolean;
  lockoutEnabled: boolean;
  isLockedOut: boolean;
  concurrencyStamp?: string;
}
