﻿using Volo.Abp.Settings;

namespace HRManagement.Settings;

public class HRManagementSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(HRManagementSettings.MySetting1));
    }
}
