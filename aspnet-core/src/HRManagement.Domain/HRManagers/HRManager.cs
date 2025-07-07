using Volo.Abp.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace HRManagement.HRManagers
{
    public abstract class HRManagerBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? Department { get; set; }

        [CanBeNull]
        public virtual string? HRNumber { get; set; }
        public Guid? IdentityUserId { get; set; }

        protected HRManagerBase()
        {

        }

        public HRManagerBase(Guid id, Guid? identityUserId, string? department = null, string? hRNumber = null)
        {

            Id = id;
            Check.Length(department, nameof(department), HRManagerConsts.DepartmentMaxLength, HRManagerConsts.DepartmentMinLength);
            Check.Length(hRNumber, nameof(hRNumber), HRManagerConsts.HRNumberMaxLength, HRManagerConsts.HRNumberMinLength);
            Department = department;
            HRNumber = hRNumber;
            IdentityUserId = identityUserId;
        }

    }
}