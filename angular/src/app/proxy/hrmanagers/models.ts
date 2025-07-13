import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { IdentityUserDto } from '../volo/abp/identity/models';

export interface GetHRManagersInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  department?: string;
  hrNumber?: string;
  identityUserId?: string;
}

export interface HRManagerCreateDto {
  department?: string;
  hrNumber?: string;
  identityUserId?: string;
}

export interface HRManagerDto extends FullAuditedEntityDto<string> {
  items : any;
  department?: string;
  hrNumber?: string;
  identityUserId?: string;
  concurrencyStamp?: string;
}

export interface HRManagerExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  department?: string;
  hrNumber?: string;
  identityUserId?: string;
}

export interface HRManagerUpdateDto {
  department?: string;
  hrNumber?: string;
  identityUserId?: string;
  concurrencyStamp?: string;
}

export interface HRManagerWithNavigationPropertiesDto {
  hrManager: HRManagerDto;
  identityUser: IdentityUserDto;
}
