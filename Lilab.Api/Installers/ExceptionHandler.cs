using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Lilab.Api.Installers;

    public class ExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            SetExceptionResult(context, exception);
            context.ExceptionHandled = true;
        }

        private void SetExceptionResult(
            ExceptionContext context,
            Exception exception,
            
            HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            var errorMessage = exception.Message;

            if (exception is DbUpdateException) errorMessage = "Datos suministrados ya existen. Revise nuevamente por favor";

            var result = new JsonResult(new
            {
                error = errorMessage
            });
            context.Result = result;
            context.HttpContext.Response.StatusCode = (int)statusCode;
        }
    }
