using HRManagement;
using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace HRManagement.PayrollRecords
{
    public abstract class PayrollRecordDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal LeaveDeductions { get; set; }
        public decimal NetPay { get; set; }
        public PayrollRecordStatus Status { get; set; }
        public string? PayslipUrl { get; set; }
        public Guid EmployeeId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}