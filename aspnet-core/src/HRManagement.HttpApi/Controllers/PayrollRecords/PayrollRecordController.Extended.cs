using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HRManagement.PayrollRecords;

namespace HRManagement.Controllers.PayrollRecords
{
    [RemoteService]
    [Area("app")]
    [ControllerName("PayrollRecord")]
    [Route("api/app/payroll-records")]

    public class PayrollRecordController : PayrollRecordControllerBase, IPayrollRecordsAppService
    {
        public PayrollRecordController(IPayrollRecordsAppService payrollRecordsAppService) : base(payrollRecordsAppService)
        {
        }
    }
}