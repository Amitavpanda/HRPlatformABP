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

namespace HRManagement.AttendanceLogs
{
    public class EfCoreAttendanceLogRepository : EfCoreAttendanceLogRepositoryBase, IAttendanceLogRepository
    {
        public EfCoreAttendanceLogRepository(IDbContextProvider<HRManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}