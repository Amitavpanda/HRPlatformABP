using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.LeaveRequests
{
    public class CreateLeaveRequestResultDto
    {
        public Guid? Id { get; set; } // Add this property

        public string WorkflowStatus { get; set; }
        public LeaveRequestDto LeaveRequest { get; set; }
        public string Message { get; set; }
    }
}
