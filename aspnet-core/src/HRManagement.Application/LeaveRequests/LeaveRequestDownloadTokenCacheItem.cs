using System;

namespace HRManagement.LeaveRequests;

public abstract class LeaveRequestDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}