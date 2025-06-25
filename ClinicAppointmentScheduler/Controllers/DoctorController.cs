using BLL.DTOs;
using BLL.Services;
using BLL.Utils;
using ClinicAppointmentScheduler.Auth;
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

        /*
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
        */

        [AdminOnly]
        [HttpPost]
        [Route("admin/create")]
        public HttpResponseMessage create(DoctorRegistrationDTO doctor)
        {
            var adminId = (int)Request.Properties["UserId"];
            var result = DoctorService.Register(doctor);
            if (result)
            {
                AdminLogger.Log(adminId, "createDoctor", $"New Doctor Added");
                return Request.CreateResponse(HttpStatusCode.Created, "Doctor Registration is successfull");
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Registration failed");
        }


        [HttpPost]
        [Route("register")]
        public HttpResponseMessage register(DoctorRegistrationDTO doctor)
        {
            var result = DoctorService.Register(doctor);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.Created, "Doctor Registered successfully");
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Registration failed");
        }

        [HttpPost]
        [Route("search")]
        public HttpResponseMessage SearchDoctors(DoctorSearchDTO filter)
        {
            var result = DoctorService.Search(filter);
            if (result.Count == 0 )
            return Request.CreateResponse(HttpStatusCode.NotFound, "Sorry, No Doctor found!");

            return Request.CreateResponse(HttpStatusCode.OK, result);
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
