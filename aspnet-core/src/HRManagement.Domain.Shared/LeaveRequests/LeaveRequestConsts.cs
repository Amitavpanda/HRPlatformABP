namespace HRManagement.LeaveRequests
{
    public static class LeaveRequestConsts
    {
        private const string DefaultSorting = "{0}CreationTime desc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "LeaveRequest." : string.Empty);
        }

        public const int WorkflowInstanceIdMinLength = 0;
        public const int WorkflowInstanceIdMaxLength = 1000;
    }
}