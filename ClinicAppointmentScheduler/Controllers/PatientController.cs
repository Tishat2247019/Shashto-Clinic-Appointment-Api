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
    [RoutePrefix("api/patient")]
    public class PatientController : ApiController
    {
        [AdminOnly]
        [HttpGet]
        [Route("admin/all")]
        public HttpResponseMessage Get()
        {
            var data = PatientService.Get();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [AdminOnly]
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

        [Logged]
        [HttpGet]
        [Route("get/me")]
        public HttpResponseMessage GetMe()
        {
            var userId = (int)Request.Properties["UserId"];
            var userType = Request.Properties["UserType"].ToString();

            if (userType == "Patient")
            {
                var data = PatientService.Get(userId);
                if (data == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Patient not found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.Forbidden, "You are not authorized to access this");

        }

        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register(PatientRegistrationDTO patient)
        {
            var result = PatientService.Register(patient);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.Created, "Patient registered successfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Registration failed");
        }

        [AdminOnly]
        [HttpPost]
        [Route("admin/create")]
        public HttpResponseMessage Create(PatientRegistrationDTO patient)
        {
            var adminId = (int)Request.Properties["UserId"];
            var result = PatientService.Register(patient);
            if (result)
            {
                AdminLogger.Log(adminId, "createPatient", $"New Patient Added");
                return Request.CreateResponse(HttpStatusCode.Created, "Patient creation successfull");
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Patient Creation failed");
        }


        [Logged]
        [HttpPatch]
        [Route("update")]
        public HttpResponseMessage Update(PatientDTO patient)
        {
            var userId = (int)Request.Properties["UserId"];
            var userType = Request.Properties["UserType"].ToString();

            if (userType == "Patient")
            {
                patient.Id = userId;
                var result = PatientService.Update(patient);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Patient updated successfully");
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Patient not found or update failed");
            }

            return Request.CreateResponse(HttpStatusCode.Forbidden, "You are not authorized to update this patient's information.");
        }


        [Logged]
        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
          /*  if (!Request.Properties.ContainsKey("UserId") || !Request.Properties.ContainsKey("UserType"))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Missing authentication info");
            }
          */

            var userId = (int)Request.Properties["UserId"];
            var userType = Request.Properties["UserType"].ToString();

            if (userType == "Admin" || userId == id)
            {
                bool status = PatientService.Delete(id);
                if (status)
                {
                    if (userType == "Admin")
                    {
                        AdminLogger.Log(userId, "DeletePatient", $"Existing Patient Deleted");
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, "Patient deleted successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Patient Id not found ");

                }

            }

            return Request.CreateResponse(HttpStatusCode.Forbidden, "You are not authorized to delete this patient.");
        }

    }
}
