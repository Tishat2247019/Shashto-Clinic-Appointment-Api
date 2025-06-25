using BLL.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ClinicAppointmentScheduler.Auth
{
    public class AdminOnly : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var header = actionContext.Request.Headers.Authorization;

            if (header == null)
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized, "No token supplied");
                return;
            }

            var tokenKey = header.Parameter;

            /*
             * Console.WriteLine(tokenKey); 
             */

            var tokenDto = AuthService.GetToken(tokenKey);

            if (tokenDto == null || tokenDto.ExpiredAt != null)
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized, "Supplied token is invalid or expired");
                return;
            }

            if(tokenDto.UserType != "Admin")
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized, "Only admin can access this endpoint");
                return;
            }

            actionContext.Request.Properties["UserId"] = tokenDto.UserId;
            actionContext.Request.Properties["UserType"] = tokenDto.UserType;

            base.OnAuthorization(actionContext);
        }
    }
}
