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
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        [AdminOnly]
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage Get()
        {
            var data = AdminService.Get();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [AdminOnly]
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var data = AdminService.Get(id);
            if (data == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Admin not found");

            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [AdminOnly]
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(AdminDTO admin)
        {
            var result = AdminService.Create(admin);
            if (result)
                return Request.CreateResponse(HttpStatusCode.Created, "Admin created successfully");

            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Creation failed");
        }

        [AdminOnly]
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(AdminDTO admin)
        {
            var result = AdminService.Update(admin);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, "Admin updated successfully");

            return Request.CreateResponse(HttpStatusCode.NotFound, "Admin not found or update failed");
        }

        [AdminOnly]
        [HttpGet]
        [Route("analytics/appointments")]
        public HttpResponseMessage GetAppointmentStats()
        {

            var adminId = (int)Request.Properties["UserId"];
            var stats = AppointmentService.GetStats();
            if (stats != null)
            {
                AdminLogger.Log(adminId, "showAppointAnalytics", $"Check all the apointments analytics");
                return Request.CreateResponse(HttpStatusCode.OK, stats);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Appointsment stats not found");

            }
        }

        [AdminOnly]
        [HttpGet]
        [Route("analytics/patient-engagement")]
        public HttpResponseMessage GetPatientEngagement()
        {
            var adminId = (int)Request.Properties["UserId"];
            var data = PatientService.GetEngagementStats();
            if(data != null)
            {
                AdminLogger.Log(adminId, "showPatientEngagment", $"Show all patientEngments");

                return Request.CreateResponse(HttpStatusCode.OK, data);

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Patient Engagements not found");

            }
        }

        [AdminOnly]
        [HttpGet]
        [Route("activity")]
        public HttpResponseMessage GetAdminActivities()
        {
            var adminId = (int)Request.Properties["UserId"];
            var allActivity = AdminActivityService.GetAllLog();
            if (allActivity != null)
            {
                AdminLogger.Log(adminId, "showAdminActivity", $"Checked all Admin Activity");

                return Request.CreateResponse(HttpStatusCode.OK, allActivity);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Admin Activity not found");

            }
        }

        [AdminOnly]
        [HttpGet]
        [Route("activity/download")]
        public HttpResponseMessage DownloadLogPdf()
        {
            //var userName = Request.Properties["UserName"]?.ToString() ?? "Unknown";
            var adminId = (int)Request.Properties["UserId"];

            var data = AdminService.Get(adminId);

            var pdfBytes = AdminActivityService.GenerateActivityLogPdf(data.Name);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(pdfBytes)
            };

            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = $"AdminLog_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
            };

            return result;
        }

        [AdminOnly]
        [HttpGet]
        [Route("activity/{id}")]
        public HttpResponseMessage GetAdminActivitiesById(int id)
        {
            var adminId = (int)Request.Properties["UserId"];
            var adminActivity = AdminActivityService.GetLogsByAdmin(id);
            if (adminActivity != null)
            {
                AdminLogger.Log(adminId, "showAdminActivityById", $"Checked specific Admin Activity");

                return Request.CreateResponse(HttpStatusCode.OK, adminActivity);

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Admin Activity not found for this admin");

            }
        }


        [AdminOnly]
        [HttpPost]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var deleted = AdminService.Delete(id);
            if (deleted)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Admin deleted successfully");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Admin with ID {id} not found");
            }
        }
    }
}
