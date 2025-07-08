using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HRManagement.AttendanceLogs;

namespace HRManagement.Controllers.AttendanceLogs
{
    [RemoteService]
    [Area("app")]
    [ControllerName("AttendanceLog")]
    [Route("api/app/attendance-logs")]

    public class AttendanceLogController : AttendanceLogControllerBase, IAttendanceLogsAppService
    {
        public AttendanceLogController(IAttendanceLogsAppService attendanceLogsAppService) : base(attendanceLogsAppService)
        {
        }
    }
}