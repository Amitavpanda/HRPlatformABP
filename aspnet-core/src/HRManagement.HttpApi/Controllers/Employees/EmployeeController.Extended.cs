using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HRManagement.Employees;

namespace HRManagement.Controllers.Employees
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Employee")]
    [Route("api/app/employees")]

    public class EmployeeController : EmployeeControllerBase, IEmployeesAppService
    {
        public EmployeeController(IEmployeesAppService employeesAppService) : base(employeesAppService)
        {
        }
    }
}