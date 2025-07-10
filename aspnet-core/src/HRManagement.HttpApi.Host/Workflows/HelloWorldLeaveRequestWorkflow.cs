using System.Net;
using Elsa.Http;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Microsoft.AspNetCore.Http;

namespace HRManagement.Workflows
{
    public class HelloWorldLeaveRequestWorkflow : WorkflowBase
    {
        protected override void Build(IWorkflowBuilder builder)
        {
            builder.Root = new Sequence
            {
                Activities =
            {
                new HttpEndpoint
                {
                    Path = new("leave-requests/hello"),
                    SupportedMethods = new(new[] { HttpMethods.Get }),
                    CanStartWorkflow = true
                },
                new WriteHttpResponse
                {
                    Content = new("hello world leave request is called"),
                    StatusCode = new(HttpStatusCode.OK)
                }
            }
            };
        }
    }
}
