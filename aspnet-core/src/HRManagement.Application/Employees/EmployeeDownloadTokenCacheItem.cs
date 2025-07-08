using System;

namespace HRManagement.Employees;

public abstract class EmployeeDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}