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
    [RoutePrefix("api/doctor")]
    public class DoctorController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage Get()
        {
            var data = DoctorService.Get();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("get/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var data = DoctorService.Get(id);
            if (data == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Doctor not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(DoctorDTO doctor)
        {
            var result = DoctorService.Create(doctor);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.Created, "Doctor created successfully");
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Creation failed");
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(DoctorDTO doctor)
        {
            var result = DoctorService.Update(doctor);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Doctor updated successfully");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Doctor not found or update failed");
        }

        [HttpPost]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            DoctorService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Doctor deleted successfully");
        }
    }
}
