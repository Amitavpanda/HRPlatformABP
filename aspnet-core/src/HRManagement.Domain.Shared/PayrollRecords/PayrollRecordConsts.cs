namespace HRManagement.PayrollRecords
{
    public static class PayrollRecordConsts
    {
        private const string DefaultSorting = "{0}CreationTime desc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "PayrollRecord." : string.Empty);
        }

        public const int MonthMinLength = 0;
        public const int MonthMaxLength = 13;
        public const int YearMinLength = 0;
        public const int YearMaxLength = 10000;
    }
}