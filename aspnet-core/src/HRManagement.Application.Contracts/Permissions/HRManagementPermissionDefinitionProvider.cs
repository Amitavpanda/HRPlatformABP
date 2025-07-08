using HRManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace HRManagement.Permissions;

public class HRManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HRManagementPermissions.GroupName);

        myGroup.AddPermission(HRManagementPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(HRManagementPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(HRManagementPermissions.MyPermission1, L("Permission:MyPermission1"));

        var hRManagerPermission = myGroup.AddPermission(HRManagementPermissions.HRManagers.Default, L("Permission:HRManagers"));
        hRManagerPermission.AddChild(HRManagementPermissions.HRManagers.Create, L("Permission:Create"));
        hRManagerPermission.AddChild(HRManagementPermissions.HRManagers.Edit, L("Permission:Edit"));
        hRManagerPermission.AddChild(HRManagementPermissions.HRManagers.Delete, L("Permission:Delete"));

        var employeePermission = myGroup.AddPermission(HRManagementPermissions.Employees.Default, L("Permission:Employees"));
        employeePermission.AddChild(HRManagementPermissions.Employees.Create, L("Permission:Create"));
        employeePermission.AddChild(HRManagementPermissions.Employees.Edit, L("Permission:Edit"));
        employeePermission.AddChild(HRManagementPermissions.Employees.Delete, L("Permission:Delete"));

        var attendanceLogPermission = myGroup.AddPermission(HRManagementPermissions.AttendanceLogs.Default, L("Permission:AttendanceLogs"));
        attendanceLogPermission.AddChild(HRManagementPermissions.AttendanceLogs.Create, L("Permission:Create"));
        attendanceLogPermission.AddChild(HRManagementPermissions.AttendanceLogs.Edit, L("Permission:Edit"));
        attendanceLogPermission.AddChild(HRManagementPermissions.AttendanceLogs.Delete, L("Permission:Delete"));

        var leaveRequestPermission = myGroup.AddPermission(HRManagementPermissions.LeaveRequests.Default, L("Permission:LeaveRequests"));
        leaveRequestPermission.AddChild(HRManagementPermissions.LeaveRequests.Create, L("Permission:Create"));
        leaveRequestPermission.AddChild(HRManagementPermissions.LeaveRequests.Edit, L("Permission:Edit"));
        leaveRequestPermission.AddChild(HRManagementPermissions.LeaveRequests.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HRManagementResource>(name);
    }
}