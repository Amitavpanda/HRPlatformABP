using HRManagement.Employees;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using HRManagement.PayrollRecords;

namespace HRManagement.PayrollRecords
{
    public class PayrollRecordsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IPayrollRecordRepository _payrollRecordRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly EmployeesDataSeedContributor _employeesDataSeedContributor;

        public PayrollRecordsDataSeedContributor(IPayrollRecordRepository payrollRecordRepository, IUnitOfWorkManager unitOfWorkManager, EmployeesDataSeedContributor employeesDataSeedContributor)
        {
            _payrollRecordRepository = payrollRecordRepository;
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

            await _payrollRecordRepository.InsertAsync(new PayrollRecord
            (
                id: Guid.Parse("da58d51c-e5f7-4a51-b512-7ba283898446"),
                month: 7,
                year: 8008,
                baseSalary: 231496432,
                leaveDeductions: 2135367067,
                netPay: 479774778,
                status: default,
                payslipUrl: "44eddc9ee6004901ab47dda6c61e55eac0e706f4a6a",
                employeeId: Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc")
            ));

            await _payrollRecordRepository.InsertAsync(new PayrollRecord
            (
                id: Guid.Parse("0a21286c-3f18-4271-91f7-401c11f19fed"),
                month: 5,
                year: 5744,
                baseSalary: 1820067896,
                leaveDeductions: 243761204,
                netPay: 551737165,
                status: default,
                payslipUrl: "792239ce64c14c9b968e2bc53e38279f8429a4f110424c77a6a5e",
                employeeId: Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc")
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}