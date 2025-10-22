using BarberFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarberFlow.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BarberFlowException)
        {
            HandleProjectException(context);
        }

        else
        {
            HandleUnknownException(context);
        }
    }

    private static void HandleProjectException(ExceptionContext context)
    {
        var barberFlowException = (BarberFlowException)context.Exception;
        var errorResponse = new ResponseErrorJson(barberFlowException.GetErrors());

        context.HttpContext.Response.StatusCode = barberFlowException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private static void HandleUnknownException(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson("Ocorreu um erro inesperado.");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}