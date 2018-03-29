using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace TaskSolution.ExceptionFilters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An unhandled exception was thrown by Customer Web API controller."),
                ReasonPhrase = "An unhandled exception was thrown by Customer Web API controller."
            };
            context.Response = msg;
        }
    }
}