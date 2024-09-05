using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        // Create a Standardised ProblemDetails to contain our error response...
        var errorResponse = new ProblemDetails
        {
            Type = exception.GetType().Name,
            Detail = exception.Message
        };

        //set the properties appropriatley for each exception we're looking for
        switch (exception)
        {
            case UnauthorizedAccessException:
                errorResponse.Status = (int)HttpStatusCode.Unauthorized;
                errorResponse.Title = exception.GetType().Name;
                break;

            case KeyNotFoundException:
                errorResponse.Status = (int)HttpStatusCode.NotFound;
                errorResponse.Title = exception.GetType().Name;
                break;

            default:
                errorResponse.Status = (int)HttpStatusCode.InternalServerError;
                errorResponse.Title = "Internal Server Error";
                break;
        }

        //set the return status
        httpContext.Response.StatusCode = errorResponse.Status.Value;
        
        //write out the response in json format
        await httpContext
            .Response
            .WriteAsJsonAsync(errorResponse, cancellationToken);
        
        return true;
    }
}