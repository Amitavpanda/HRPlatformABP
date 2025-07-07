using System;

namespace HRManagement.HRManagers;

public abstract class HRManagerDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}