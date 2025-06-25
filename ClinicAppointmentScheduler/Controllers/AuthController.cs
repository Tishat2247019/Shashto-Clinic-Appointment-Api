using BLL.Services;
using ClinicAppointmentScheduler.Auth;
using ClinicAppointmentScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClinicAppointmentScheduler.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("api/login")]
        public HttpResponseMessage Login(LoginModel login)
        {
            var token = AuthService.Auth(login.Email, login.Password);
            if (token != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid email or password");
        }

        [Logged]
        [HttpPost]
        [Route("api/logout")]
        public HttpResponseMessage Logout()
        {
            var tokenKey = Request.Headers.Authorization?.ToString();
            var updated = AuthService.Logout(tokenKey);
            return Request.CreateResponse(HttpStatusCode.OK, updated);
        }
    }
}
