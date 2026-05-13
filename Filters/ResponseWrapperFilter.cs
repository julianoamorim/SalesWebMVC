

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SalesWebMVC.Models;

namespace SalesWebMVC.Filters
{
    public class ResponseWrapperFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context){}

        public void OnResultExecuting(ResultExecutingContext context)
    {
        if(context.Result is ObjectResult objectResult &&
           objectResult.StatusCode >= 200 && objectResult.StatusCode < 300)
        {
            int statusCode = objectResult.StatusCode ?? 200;

            var wrappedResponse = new ResponseWrapper<object>
            {
                Status = statusCode,
                Data = objectResult.Value
            };

            context.Result = new ObjectResult(wrappedResponse)
            {
                StatusCode = statusCode
            };
        }
    }
    }
}