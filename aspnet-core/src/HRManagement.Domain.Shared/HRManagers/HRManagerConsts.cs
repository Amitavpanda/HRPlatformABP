namespace HRManagement.HRManagers
{
    public static class HRManagerConsts
    {
        private const string DefaultSorting = "{0}CreationTime desc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "HRManager." : string.Empty);
        }

        public const int DepartmentMinLength = 0;
        public const int DepartmentMaxLength = 200;
        public const int HRNumberMinLength = 0;
        public const int HRNumberMaxLength = 200;
    }
}