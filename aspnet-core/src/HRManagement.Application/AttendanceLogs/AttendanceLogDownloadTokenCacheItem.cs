using System;

namespace HRManagement.AttendanceLogs;

public abstract class AttendanceLogDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}