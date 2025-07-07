using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace HRManagement.HRManagers
{
    public abstract class HRManagerUpdateDtoBase : IHasConcurrencyStamp
    {
        [StringLength(HRManagerConsts.DepartmentMaxLength, MinimumLength = HRManagerConsts.DepartmentMinLength)]
        public string? Department { get; set; }
        [StringLength(HRManagerConsts.HRNumberMaxLength, MinimumLength = HRManagerConsts.HRNumberMinLength)]
        public string? HRNumber { get; set; }
        public Guid? IdentityUserId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}