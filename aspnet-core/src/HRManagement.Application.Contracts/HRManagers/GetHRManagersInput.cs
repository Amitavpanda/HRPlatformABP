using Volo.Abp.Application.Dtos;
using System;

namespace HRManagement.HRManagers
{
    public abstract class GetHRManagersInputBase : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Department { get; set; }
        public string? HRNumber { get; set; }
        public Guid? IdentityUserId { get; set; }

        public GetHRManagersInputBase()
        {

        }
    }
}