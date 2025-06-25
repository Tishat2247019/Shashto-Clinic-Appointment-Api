using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClinicAppointmentScheduler.Controllers
{
    [RoutePrefix("api/appointment")]
    public class AppointmentController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage Get()
        {
            var data = AppointmentService.Get();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("get/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var data = AppointmentService.Get(id);
            if (data == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Appointment not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(AppointmentDTO appointment)
        {
            var result = AppointmentService.Create(appointment);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.Created, "Appointment created successfully");
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Creation failed");
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(AppointmentDTO appointment)
        {
            var result = AppointmentService.Update(appointment);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Appointment updated successfully");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Appointment not found or update failed");
        }

        [HttpPost]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            AppointmentService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Appointment deleted successfully");
        }
    }
}
