using BLL.DTOs;
using BLL.Services;
using BLL.Utils;
using ClinicAppointmentScheduler.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        [AdminOnly]
        [HttpGet]
        [Route("admin/get/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var data = AppointmentService.Get(id);
            if (data == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Appointment not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Logged]
        [HttpGet]
        [Route("get/me")]
        public HttpResponseMessage GetAppointLoggedIn()
        {
            var userId = (int)Request.Properties["UserId"];
            var data = AppointmentService.Get(userId);
            if (data == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Appointment not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        [AdminOnly]
        [HttpPost]
        [Route("admin/create")]
        public async Task<HttpResponseMessage> Create(AppointmentDTO dto)
        {
            try
            {
                /* var userId = (int)Request.Properties["UserId"];
                 var userType = Request.Properties["UserType"].ToString();

                 if (userType != "Admin")
                     return Request.CreateResponse(HttpStatusCode.Forbidden, "Only Admins can create new Appointment");
                */

                // dto.PatientId = userId;

                var adminId = (int)Request.Properties["UserId"];

                var success = await AppointmentService.CreateWithToken(dto);

                if (success)
                {
                    AdminLogger.Log(adminId, "createAppointment", $"New Appointment Created");
                    return Request.CreateResponse(HttpStatusCode.Created, "An appointment has been created for this patient");

                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Appointment creation failed");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Logged]
        [HttpPost]
        [Route("book")]
        public async Task<HttpResponseMessage> CreateAuthenticated(AppointmentDTO dto)
        {
            try
            {
                var userId = (int)Request.Properties["UserId"];
                var userType = Request.Properties["UserType"].ToString();

                if (userType != "Patient")
                    return Request.CreateResponse(HttpStatusCode.Forbidden, "Only logged in patients can book appointments");

                dto.PatientId = userId;
                var success = await AppointmentService.CreateWithToken(dto);

                if (success) return Request.CreateResponse(HttpStatusCode.Created, "Congratulations! Your Appointment has been created. Please check you email for the appoiontment token");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Appointment creation failed");
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


       /* [Logged]
        [HttpPost]
        [Route("book")]
        public HttpResponseMessage BookAsPatient(AppointmentDTO dto)
        {
            var tokenType = Request.Properties["UserType"]?.ToString();
            var tokenUserId = (int)Request.Properties["UserId"];

            // Ensure only patient can book via this endpoint
            if (tokenType != "Patient")
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Only patients can book appointments.");
            }

            // Override patient ID from token
            dto.PatientId = tokenUserId;

            var success = AppointmentService.Create(dto);
            if (success)
            {
                return Request.CreateResponse(HttpStatusCode.Created, "Appointment booked successfully");
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Failed to book appointment");
        }
       */

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
