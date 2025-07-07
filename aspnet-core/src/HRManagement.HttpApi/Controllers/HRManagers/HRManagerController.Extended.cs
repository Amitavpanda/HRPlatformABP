using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HRManagement.HRManagers;

namespace HRManagement.Controllers.HRManagers
{
    [RemoteService]
    [Area("app")]
    [ControllerName("HRManager")]
    [Route("api/app/hrmanagers")]

    public class HRManagerController : HRManagerControllerBase, IHRManagersAppService
    {
        public HRManagerController(IHRManagersAppService hRManagersAppService) : base(hRManagersAppService)
        {
        }
    }
}