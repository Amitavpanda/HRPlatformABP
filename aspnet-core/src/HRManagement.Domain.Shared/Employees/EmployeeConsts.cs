namespace HRManagement.Employees
{
    public static class EmployeeConsts
    {
        private const string DefaultSorting = "{0}CreationTime desc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Employee." : string.Empty);
        }

        public const int EmployeeNumberMinLength = 0;
        public const int EmployeeNumberMaxLength = 100;
        public const decimal PaidLeaveBalanceMinLength = 0;
        public const decimal PaidLeaveBalanceMaxLength = 1000;
        public const decimal UnpaidLeaveBalanceMinLength = 0;
        public const decimal UnpaidLeaveBalanceMaxLength = 1000;
        public const decimal SickLeaveBalanceMinLength = 0;
        public const decimal SickLeaveBalanceMaxLength = 1000;
        public const decimal DeductionPerDayMinLength = 0;
        public const decimal DeductionPerDayMaxLength = 1000;
    }
}