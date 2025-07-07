using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace HRManagement.HRManagers
{
    public abstract class HRManagerManagerBase : DomainService
    {
        protected IHRManagerRepository _hRManagerRepository;

        public HRManagerManagerBase(IHRManagerRepository hRManagerRepository)
        {
            _hRManagerRepository = hRManagerRepository;
        }

        public virtual async Task<HRManager> CreateAsync(
        Guid? identityUserId, string? department = null, string? hRNumber = null)
        {
            Check.Length(department, nameof(department), HRManagerConsts.DepartmentMaxLength, HRManagerConsts.DepartmentMinLength);
            Check.Length(hRNumber, nameof(hRNumber), HRManagerConsts.HRNumberMaxLength, HRManagerConsts.HRNumberMinLength);

            var hRManager = new HRManager(
             GuidGenerator.Create(),
             identityUserId, department, hRNumber
             );

            return await _hRManagerRepository.InsertAsync(hRManager);
        }

        public virtual async Task<HRManager> UpdateAsync(
            Guid id,
            Guid? identityUserId, string? department = null, string? hRNumber = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.Length(department, nameof(department), HRManagerConsts.DepartmentMaxLength, HRManagerConsts.DepartmentMinLength);
            Check.Length(hRNumber, nameof(hRNumber), HRManagerConsts.HRNumberMaxLength, HRManagerConsts.HRNumberMinLength);

            var hRManager = await _hRManagerRepository.GetAsync(id);

            hRManager.IdentityUserId = identityUserId;
            hRManager.Department = department;
            hRManager.HRNumber = hRNumber;

            hRManager.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _hRManagerRepository.UpdateAsync(hRManager);
        }

    }
}