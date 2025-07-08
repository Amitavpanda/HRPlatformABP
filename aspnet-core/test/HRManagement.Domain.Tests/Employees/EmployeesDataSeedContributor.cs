using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using HRManagement.Employees;

namespace HRManagement.Employees
{
    public class EmployeesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public EmployeesDataSeedContributor(IEmployeeRepository employeeRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _employeeRepository = employeeRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _employeeRepository.InsertAsync(new Employee
            (
                id: Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc"),
                employeeNumber: "77bbf33250304481a8dee3b99b80cb480f728e16f58c46d496de96a2f9f221859e1eab6b9fb840808bf2f587f1cc1ce987b7",
                dateOfJoining: new DateTime(2020, 11, 19),
                leaveBalance: 186,
                baseSalary: 1155384050,
                identityUserId: null
            ));

            await _employeeRepository.InsertAsync(new Employee
            (
                id: Guid.Parse("e0ec921f-6948-47f7-ac3b-9bab00eb724f"),
                employeeNumber: "78f849fd599d45d5841f222d049470c43007e3c9dd7a49a8875989aadfdfc3865dd9aee7c6544deca97f1947fd13e23084a6",
                dateOfJoining: new DateTime(2018, 1, 9),
                leaveBalance: 790,
                baseSalary: 2112231930,
                identityUserId: null
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}