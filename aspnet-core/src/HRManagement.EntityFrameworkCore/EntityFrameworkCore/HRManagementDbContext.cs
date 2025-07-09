using HRManagement.PayrollRecords;
using HRManagement.LeaveRequests;
using HRManagement.AttendanceLogs;
using HRManagement.Employees;
using HRManagement.HRManagers;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict.EntityFrameworkCore;

namespace HRManagement.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class HRManagementDbContext :
    AbpDbContext<HRManagementDbContext>,
    IIdentityProDbContext,
    ISaasDbContext
{
    public DbSet<PayrollRecord> PayrollRecords { get; set; } = null!;
    public DbSet<LeaveRequest> LeaveRequests { get; set; } = null!;
    public DbSet<AttendanceLog> AttendanceLogs { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<HRManager> HRManagers { get; set; } = null!;
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public HRManagementDbContext(DbContextOptions<HRManagementDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddictPro();
        builder.ConfigureFeatureManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureGdpr();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(HRManagementConsts.DbTablePrefix + "YourEntities", HRManagementConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<HRManager>(b =>
            {
                b.ToTable(HRManagementConsts.DbTablePrefix + "HRManagers", HRManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Department).HasColumnName(nameof(HRManager.Department)).HasMaxLength(HRManagerConsts.DepartmentMaxLength);
                b.Property(x => x.HRNumber).HasColumnName(nameof(HRManager.HRNumber)).HasMaxLength(HRManagerConsts.HRNumberMaxLength);
                b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.IdentityUserId).OnDelete(DeleteBehavior.SetNull);
            });

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Employee>(b =>
            {
                b.ToTable(HRManagementConsts.DbTablePrefix + "Employees", HRManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.EmployeeNumber).HasColumnName(nameof(Employee.EmployeeNumber)).HasMaxLength(EmployeeConsts.EmployeeNumberMaxLength);
                b.Property(x => x.DateOfJoining).HasColumnName(nameof(Employee.DateOfJoining));
                b.Property(x => x.LeaveBalance).HasColumnName(nameof(Employee.LeaveBalance)).HasMaxLength((int)EmployeeConsts.LeaveBalanceMaxLength);
                b.Property(x => x.BaseSalary).HasColumnName(nameof(Employee.BaseSalary));
                b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.IdentityUserId).OnDelete(DeleteBehavior.SetNull);
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<AttendanceLog>(b =>
            {
                b.ToTable(HRManagementConsts.DbTablePrefix + "AttendanceLogs", HRManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Date).HasColumnName(nameof(AttendanceLog.Date));
                b.Property(x => x.CheckInTime).HasColumnName(nameof(AttendanceLog.CheckInTime));
                b.Property(x => x.CheckOutTime).HasColumnName(nameof(AttendanceLog.CheckOutTime));
                b.Property(x => x.Status).HasColumnName(nameof(AttendanceLog.Status));
                b.HasOne<Employee>().WithMany().IsRequired().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.NoAction);
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<LeaveRequest>(b =>
            {
                b.ToTable(HRManagementConsts.DbTablePrefix + "LeaveRequests", HRManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.LeaveRequestType).HasColumnName(nameof(LeaveRequest.LeaveRequestType));
                b.Property(x => x.StartDate).HasColumnName(nameof(LeaveRequest.StartDate));
                b.Property(x => x.EndDate).HasColumnName(nameof(LeaveRequest.EndDate));
                b.Property(x => x.LeaveRequestStatus).HasColumnName(nameof(LeaveRequest.LeaveRequestStatus));
                b.Property(x => x.RequestedOn).HasColumnName(nameof(LeaveRequest.RequestedOn));
                b.Property(x => x.ReviewedOn).HasColumnName(nameof(LeaveRequest.ReviewedOn));
                b.Property(x => x.WorkflowInstanceId).HasColumnName(nameof(LeaveRequest.WorkflowInstanceId)).HasMaxLength(LeaveRequestConsts.WorkflowInstanceIdMaxLength);
                b.HasOne<Employee>().WithMany().IsRequired().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.ReviewedBy).OnDelete(DeleteBehavior.SetNull);
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<PayrollRecord>(b =>
            {
                b.ToTable(HRManagementConsts.DbTablePrefix + "PayrollRecords", HRManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Month).HasColumnName(nameof(PayrollRecord.Month)).HasMaxLength(PayrollRecordConsts.MonthMaxLength);
                b.Property(x => x.Year).HasColumnName(nameof(PayrollRecord.Year)).HasMaxLength(PayrollRecordConsts.YearMaxLength);
                b.Property(x => x.BaseSalary).HasColumnName(nameof(PayrollRecord.BaseSalary));
                b.Property(x => x.LeaveDeductions).HasColumnName(nameof(PayrollRecord.LeaveDeductions));
                b.Property(x => x.NetPay).HasColumnName(nameof(PayrollRecord.NetPay));
                b.Property(x => x.Status).HasColumnName(nameof(PayrollRecord.Status));
                b.Property(x => x.PayslipUrl).HasColumnName(nameof(PayrollRecord.PayslipUrl));
                b.HasOne<Employee>().WithMany().IsRequired().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.NoAction);
            });

        }
    }
}