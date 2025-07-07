using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace HRManagement.HRManagers
{
    public abstract class HRManagerDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string? Department { get; set; }
        public string? HRNumber { get; set; }
        public Guid? IdentityUserId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}