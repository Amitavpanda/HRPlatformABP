using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using HRManagement.EntityFrameworkCore;

namespace HRManagement.PayrollRecords
{
    public class EfCorePayrollRecordRepository : EfCorePayrollRecordRepositoryBase, IPayrollRecordRepository
    {
        public EfCorePayrollRecordRepository(IDbContextProvider<HRManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}