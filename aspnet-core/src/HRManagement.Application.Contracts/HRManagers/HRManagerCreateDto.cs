using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HRManagement.HRManagers
{
    public abstract class HRManagerCreateDtoBase
    {
        [StringLength(HRManagerConsts.DepartmentMaxLength, MinimumLength = HRManagerConsts.DepartmentMinLength)]
        public string? Department { get; set; }
        [StringLength(HRManagerConsts.HRNumberMaxLength, MinimumLength = HRManagerConsts.HRNumberMinLength)]
        public string? HRNumber { get; set; }
        public Guid? IdentityUserId { get; set; }
    }
}