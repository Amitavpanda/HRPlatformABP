using Volo.Abp.Identity;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace HRManagement.HRManagers
{
    public abstract class HRManagerWithNavigationPropertiesDtoBase
    {
        public HRManagerDto HRManager { get; set; } = null!;

        public IdentityUserDto IdentityUser { get; set; } = null!;

    }
}