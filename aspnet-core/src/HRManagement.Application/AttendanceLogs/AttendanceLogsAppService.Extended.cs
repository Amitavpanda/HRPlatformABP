using HRManagement.Shared;
using HRManagement.Employees;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using HRManagement.Permissions;
using HRManagement.AttendanceLogs;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using HRManagement.Shared;

namespace HRManagement.AttendanceLogs
{
    public class AttendanceLogsAppService : AttendanceLogsAppServiceBase, IAttendanceLogsAppService
    {
        //<suite-custom-code-autogenerated>
        public AttendanceLogsAppService(IAttendanceLogRepository attendanceLogRepository, AttendanceLogManager attendanceLogManager, IDistributedCache<AttendanceLogDownloadTokenCacheItem, string> downloadTokenCache, IRepository<HRManagement.Employees.Employee, Guid> employeeRepository)
            : base(attendanceLogRepository, attendanceLogManager, downloadTokenCache, employeeRepository)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}