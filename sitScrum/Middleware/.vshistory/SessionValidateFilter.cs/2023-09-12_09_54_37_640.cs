using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using sitScrum.Controllers;
using sitScrum.Models.Authentication;
using sitScrum.util;
using System;

namespace sitScrum.Middleware
{
    public class SessionValidateFilter : IActionFilter
    {
        private readonly ILogger<SessionValidateFilter> _logger;

        public SessionValidateFilter(ILogger<SessionValidateFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var hhtpContext = context.HttpContext;

            try
            {

                if (!(context.Controller is UserController) &&
                    (hhtpContext.Session == null || hhtpContext.Session.GetObject<DatosUsuario>("usrData") == null))
                {

                    Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(hhtpContext);
                    hhtpContext.Session.Clear();
                    hhtpContext.Response.Redirect("/home");

                }

            }
            catch (Exception exep)
            {
                _logger.LogError("Error al eliminar cookies de autenticacion - N1" + exep.Message);
                try
                {
                    Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(hhtpContext);
                    hhtpContext.Session.Clear();
                }
                catch (Exception exep1)
                {
                    _logger.LogError("Error al eliminar cookies de autenticacion -N2" + exep1.Message);
                }
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
        }
    }
}
