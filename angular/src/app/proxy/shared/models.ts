import type { EntityDto, PagedResultRequestDto } from '@abp/ng.core';

export interface AppFileDescriptorDto extends EntityDto<string> {
  name?: string;
  mimeType?: string;
}

export interface DownloadTokenResultDto {
  token?: string;
}

export interface GetFileInput {
  downloadToken?: string;
  fileId?: string;
}

export interface LookupDto<TKey> {
  id: TKey;
  displayName?: string;
}

export interface LookupRequestDto extends PagedResultRequestDto {
  filter?: string;
}
