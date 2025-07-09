using System;

namespace HRManagement.PayrollRecords;

public abstract class PayrollRecordDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}