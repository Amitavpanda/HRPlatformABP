using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HRManagement.HRManagers
{
    public partial interface IHRManagerRepository : IRepository<HRManager, Guid>
    {

        Task DeleteAllAsync(
            string? filterText = null,
            string? department = null,
            string? hRNumber = null,
            Guid? identityUserId = null,
            CancellationToken cancellationToken = default);
        Task<HRManagerWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );

        Task<List<HRManagerWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? department = null,
            string? hRNumber = null,
            Guid? identityUserId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<HRManager>> GetListAsync(
                    string? filterText = null,
                    string? department = null,
                    string? hRNumber = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? department = null,
            string? hRNumber = null,
            Guid? identityUserId = null,
            CancellationToken cancellationToken = default);
    }
}