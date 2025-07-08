namespace HRManagement.AttendanceLogs
{
    public static class AttendanceLogConsts
    {
        private const string DefaultSorting = "{0}CreationTime desc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "AttendanceLog." : string.Empty);
        }

    }
}