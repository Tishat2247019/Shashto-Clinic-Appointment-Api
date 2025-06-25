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
    [RoutePrefix("api/patient")]
    public class PatientController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage Get()
        {
            var data = PatientService.Get();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("get/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var data = PatientService.Get(id);
            if (data == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Patient not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(PatientDTO patient)
        {
            var result = PatientService.Create(patient);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.Created, "Patient created successfully");
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Creation failed");
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(PatientDTO patient)
        {
            var result = PatientService.Update(patient);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Patient updated successfully");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Patient not found or update failed");
        }

        [HttpPost]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            PatientService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Patient deleted successfully");
        }
    }
}
