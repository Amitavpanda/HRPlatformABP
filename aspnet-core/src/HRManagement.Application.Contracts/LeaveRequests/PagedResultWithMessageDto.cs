using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HRManagement.LeaveRequests
{
    public class PagedResultWithMessageDto<T> : PagedResultDto<T>
    {
        public string Message { get; set; }
    }
}
