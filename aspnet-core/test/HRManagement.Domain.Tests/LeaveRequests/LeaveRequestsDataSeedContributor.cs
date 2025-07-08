using HRManagement.Employees;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using HRManagement.LeaveRequests;

namespace HRManagement.LeaveRequests
{
    public class LeaveRequestsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly EmployeesDataSeedContributor _employeesDataSeedContributor;

        public LeaveRequestsDataSeedContributor(ILeaveRequestRepository leaveRequestRepository, IUnitOfWorkManager unitOfWorkManager, EmployeesDataSeedContributor employeesDataSeedContributor)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _employeesDataSeedContributor = employeesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _employeesDataSeedContributor.SeedAsync(context);

            await _leaveRequestRepository.InsertAsync(new LeaveRequest
            (
                id: Guid.Parse("bffcad48-3c25-419a-9b9b-ef7023dc4f66"),
                leaveRequestType: default,
                startDate: new DateTime(2010, 8, 21),
                endDate: new DateTime(2015, 5, 1),
                leaveRequestStatus: default,
                requestedOn: new DateTime(2024, 8, 6),
                reviewedOn: new DateTime(2017, 7, 9),
                workflowInstanceId: "961a2cb744704b9db8172d825bcba63157c1ac1e2cb44bc69f56721f690aaaf90a86a4604fb844d984e4fa453f2002aba9e5f56786e448d1adf8efd5d0b7e638bc7fcba690b3426fbd6a65380f88f670f6cd44ed89904199a74529a85f8d3ce326ff51f69b574d8da571009899a9bf5b08894fdb93594f699d426c49c7639decb83cdc9a6c95419f9692c5d26e61de4fec5ba7439d604d5eae9b7b3db272887a00b42b0da4224d75a0a481ea253789a739523507508e4e74907f43163162673554b18c04fe714050bd2294ab8892da2ea7fdfa97475b4e6a95c7f47c8718221e98d24fd9b7014c3cba1aa47b961da40bf32abecdbd244fa6abc86b861e032085e1429bfe6b25461c802c3e56b9c672d55665f23f765942b1893f927246ca96ff794e3e8d3e244cc3a05ed96a7d9faffcb0717a228b9147f2ba92394ccba6fbf581562cad006c4a28a0911eaf7550afcb937326c9cd0a4cff9886667823c20bccc2a28bd550c64f759c656a9c051259552fe3115bacd74f38baf28395add87a50f0a8769ed1e04e51859f90729c810d0e6cdef69a18674756b0083872a2eb0f312193c30b969b4c53a2467ea2de44579e9c24e9918ecf482687b57ca457785e2793114a8c146c414f91ad9870e8f1e1e2d7cc61d567c74e268f4614db86d9b3e485922a2c52a346e9abbc25696bb07430a566d5e2",
                employeeId: Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc"),
                reviewedBy: null
            ));

            await _leaveRequestRepository.InsertAsync(new LeaveRequest
            (
                id: Guid.Parse("858c59ae-3c80-4f77-a163-276bf63d65a1"),
                leaveRequestType: default,
                startDate: new DateTime(2008, 9, 12),
                endDate: new DateTime(2015, 2, 4),
                leaveRequestStatus: default,
                requestedOn: new DateTime(2001, 6, 1),
                reviewedOn: new DateTime(2011, 3, 9),
                workflowInstanceId: "03036592af02483cb9318f73926d3b6b7d6ea29427654e33bd1bb22b05b330588dc48f38cf2c42dfa7d61d8d6a763e9e6daeaf66cae24e50a4fecb756afb9ffc2b2020bf4bbd47599a59e37b1fae5fe10523617cafe3458ea5da2aa8e9da45a21e4d9d896915417991f686c9fef6985f542f6c0fbf0c4732935117c47fd9c06a24593d00770a4c43a61b612e19117763998fe4d02bca4e349b08cb709bfd0680e1ba2230925d4ea8a33775c7efaa625b37593e15e1b740b68f21a90ef8f28d456ba67e0250de4438a978f950bb55cc874fd73a1460ec490b9c00dea186a26eb8f4a48e126ec24dc484ace080d7392b5155c581aedb1b4a92869b3b2d6081ec36b6436305a8694f3b80caaa527dda85311796f9acdae743ffa038b4d32edfc80791c723dc8d974ff9828db112a2a18b63d48716b9067747b19fc4b0c6ce49aed42627e1d7243d42e0bd1ce549d7105ae2a333433590c34e8abc6e9c7621ca16edb9758444884a4fc4a595f11e5acb6b63d081e6fcfcd540a8aee419b084cffa84ac153c6396ec47539fef09e9f4a6ff70bec54f09ce4e4a63a8836cee3bfbd0ec1b0255ff294d43adbcfadaea2625cb4a5d8b0ffec04c41359f7450cd2a687d258e123322c920416ca8bdb8d580bfbc79809d5c6d39334a9ebe8d394a45edc0964a074550cbf746a98dc23115e6a6dddf340f20cb",
                employeeId: Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc"),
                reviewedBy: null
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}