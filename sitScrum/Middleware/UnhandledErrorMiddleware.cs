using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;


namespace sitScrum.Middleware
{
    /// <summary>
    /// Maneja los errores no controlados
    /// </summary>
    public class UnhandledErrorMiddleware : IExceptionFilter
    {

        private readonly IModelMetadataProvider _modelMetadataProvider;

        public UnhandledErrorMiddleware(IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        /// <summary>
        /// Evento que se dispara al generarse una excepcion no controlada
        /// </summary>
        /// <param name="context">El contexto de la solicitud realizada</param>
        public virtual void OnException(ExceptionContext context)
        {
            var result = new ViewResult { ViewName = "_CustomError" };
            result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
            result.ViewData.Add("Exception", context.Exception);


            if (context.RouteData.Values.Count > 0)
            {
                result.ViewData.Add("Controller", context.RouteData.Values["controller"]);
                result.ViewData.Add("Action", context.RouteData.Values["action"]);
            }

            // Here we can pass additional detailed data via ViewData
            context.ExceptionHandled = true; // mark exception as handled
            context.Result = result;
        }
    }

    /// <summary>
    /// Maneja los errores no controlados de los json result
    /// </summary>
    public class JsonUnhandledErrorMiddleware : IExceptionFilter
    {
        /// <summary>
        /// Evento que se dispara al generarse una excepcion no controlada desde un Json Resul
        /// </summary>
        /// <param name="context">El contenido de la solicitud y respuesta del controlador</param>
        public virtual void OnException(ExceptionContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.ExceptionHandled = true;

            if (context.HttpContext.Response.StatusCode == (int)System.Net.HttpStatusCode.OK)
                context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

            context.Result = new JsonResult(new { Data = context.Exception.Message });
        }
    }
}