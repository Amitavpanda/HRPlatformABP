using Elsa;
using Elsa.Http;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Microsoft.AspNetCore.Http;
using System;
using System.Dynamic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace HRManagement.Workflows
{
    public class LeaveRequestPayload
    {
        public string EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class ValidateLeaveRequestWorkflow : WorkflowBase
    {
        protected override void Build(IWorkflowBuilder builder)
        {
            // Variables
            var bodyVar = builder.WithVariable<LeaveRequestPayload>();
            var employeeIdVar = builder.WithVariable<string>();
            var startDateVar = builder.WithVariable<DateTime>();
            var endDateVar = builder.WithVariable<DateTime>();
            var requestedDaysVar = builder.WithVariable<int>();
            var balanceVar = builder.WithVariable<int>();
            var apiResponseVar = builder.WithVariable<ExpandoObject>();

            builder.Root = new Sequence
            {
                Activities =
                {
                    // HTTP POST endpoint for leave validation
                    new HttpEndpoint
                    {
                        Path = new("leave-requests/validate"),
                        SupportedMethods = new(new[] { HttpMethods.Post }),
                        CanStartWorkflow = true,
                        ParsedContent = new(bodyVar)
                    },

                    // Log received body
                    new Inline(context =>
                    {
                        var body = bodyVar.Get(context);
                        Console.WriteLine("Workflow started: received body");
                        if (body == null)
                            Console.WriteLine("Body is null!");
                        else
                            Console.WriteLine("Body: " + JsonSerializer.Serialize(body));
                        return new ValueTask();
                    }),

                    // Extract input fields from bodyVar (strongly typed)
                    new SetVariable
                    {
                        Variable = employeeIdVar,
                        Value = new(context =>
                        {
                            var body = bodyVar.Get(context)!;
                            var employeeId = body.EmployeeId;
                            Console.WriteLine($"Extracted EmployeeId: {employeeId}");
                            return employeeId;
                        })
                    },
                    new SetVariable
                    {
                        Variable = startDateVar,
                        Value = new(context =>
                        {
                            var body = bodyVar.Get(context)!;
                            var startDate = body.StartDate;
                            Console.WriteLine($"Extracted StartDate: {startDate}");
                            return startDate;
                        })
                    },
                    new SetVariable
                    {
                        Variable = endDateVar,
                        Value = new(context =>
                        {
                            var body = bodyVar.Get(context)!;
                            var endDate = body.EndDate;
                            Console.WriteLine($"Extracted EndDate: {endDate}");
                            return endDate;
                        })
                    },
                    new Inline(context =>
                    {
                        var empId = employeeIdVar.Get(context);
                        var start = startDateVar.Get(context);
                        var end = endDateVar.Get(context);
                        Console.WriteLine($"Summary of extracted fields: EmployeeId={empId}, StartDate={start}, EndDate={end}");
                        return new ValueTask();
                    }),

                    new SetVariable
                    {
                        Variable = requestedDaysVar,
                        Value = new(context =>
                        {
                            var start = startDateVar.Get(context);
                            var end = endDateVar.Get(context);
                            var days = (int)(end.Date - start.Date).TotalDays + 1;
                            Console.WriteLine($"Calculated RequestedDays: {days}");
                            return days;
                        })
                    },
                    new Inline(context =>
                    {
                        var requestedDays = requestedDaysVar.Get(context);
                        Console.WriteLine($"RequestedDays: {requestedDays}");
                        return new ValueTask();
                    }),

                    // Call your ABP API to get leave balance
                    new SendHttpRequest
                    {
                        Url = new(context =>
                        {
                            var empId = employeeIdVar.Get(context);
                            var url = $"https://localhost:44325/api/app/leave-requests/employee-leave-balance/{empId}";
                            Console.WriteLine($"Calling leave balance API: {url}");
                            return new Uri(url);
                        }),
                        Method = new(HttpMethods.Get),
                        ParsedContent = new(apiResponseVar),
                        ExpectedStatusCodes =
                        {
                            // 200 = employee found
                            new HttpStatusCodeCase
                            {
                                StatusCode = StatusCodes.Status200OK,
                                Activity = new SetVariable
                                {
                                    Variable = balanceVar,
                                    Value = new(context =>
                                    {
                                        var apiResp = apiResponseVar.Get(context)!;
                                        Console.WriteLine("API Response for leave balance: " + JsonSerializer.Serialize(apiResp));
                                        var balance = (int)((dynamic)apiResp).balance;
                                        Console.WriteLine($"Extracted Leave Balance from API: {balance}");
                                        return balance;
                                    })
                                }
                            },
                            // 404 = employee not found
                            new HttpStatusCodeCase
                            {
                                StatusCode = StatusCodes.Status404NotFound,
                                Activity = new SetVariable
                                {
                                    Variable = balanceVar,
                                    Value = new(context =>
                                    {
                                        Console.WriteLine("Employee not found in leave balance API, setting balance to 0");
                                        return 0;
                                    })
                                }
                            }
                        }
                    },
                    new Inline(context =>
                    {
                        var bal = balanceVar.Get(context);
                        Console.WriteLine($"Leave balance: {bal}");
                        return new ValueTask();
                    }),

                    // Decision and response
                    new If
                    {
                        Condition = new(context =>
                        {
                            var balance = balanceVar.Get(context);
                            var days = requestedDaysVar.Get(context);
                            Console.WriteLine($"Decision check: balance={balance}, requestedDays={days}");
                            return balance >= days;
                        }),
                        Then = new Sequence
                        {
                            Activities =
                            {
                                new Inline(context =>
                                {
                                    Console.WriteLine("Leave request is VALID. Returning OK response.");
                                    return new ValueTask();
                                }),
                                new WriteHttpResponse
                                {
                                    Content = new(context => new { status = "Valid" }),
                                    StatusCode = new(HttpStatusCode.OK)
                                }
                            }
                        },
                        Else = new Sequence
                        {
                            Activities =
                            {
                                new Inline(context =>
                                {
                                    Console.WriteLine("Leave request is REJECTED. Returning BadRequest response.");
                                    return new ValueTask();
                                }),
                                new WriteHttpResponse
                                {
                                    Content = new(context => new { status = "Rejected" }),
                                    StatusCode = new(HttpStatusCode.BadRequest)
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}