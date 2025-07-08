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
        public const decimal LeaveBalanceMinLength = 0;
        public const decimal LeaveBalanceMaxLength = 1000;
    }
}