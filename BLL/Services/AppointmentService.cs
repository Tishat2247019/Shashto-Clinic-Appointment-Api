using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Utils;

namespace BLL.Services
{
    public class AppointmentService
    {
        private static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Appointment, AppointmentDTO>()
                    .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.Name))
                    .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.Name));

                cfg.CreateMap<AppointmentDTO, Appointment>();
            });

            return new Mapper(config);
        }

        public static List<AppointmentDTO> Get()
        {
            var data = DataAccess.AppointmentData().Get();

            // Include navigation properties manually if needed
            foreach (var appt in data)
            {
                appt.Patient = DataAccess.PatientData().Get(appt.PatientId);
                appt.Doctor = DataAccess.DoctorData().Get(appt.DoctorId);
            }

            return GetMapper().Map<List<AppointmentDTO>>(data);
        }

        public static AppointmentDTO Get(int id)
        {
            var appt = DataAccess.AppointmentData().Get(id);

            if (appt == null) return null;

            appt.Patient = DataAccess.PatientData().Get(appt.PatientId);
            appt.Doctor = DataAccess.DoctorData().Get(appt.DoctorId);

            return GetMapper().Map<AppointmentDTO>(appt);
        }

        public static bool Create(AppointmentDTO dto)
        {
            var entity = GetMapper().Map<Appointment>(dto);

            var patient = DataAccess.PatientData().Get(dto.PatientId);
            var doctor = DataAccess.DoctorData().Get(dto.DoctorId);

            string subject = "Appointment Confirmation";
            string body = $"Hello {patient.Name},\n\n" +
                          $"Your appointment with Dr. {doctor.Name} is confirmed for {dto.AppointmentDate}.\n\n" +
                          "Thank you for choosing our clinic.";

            EmailService.SendEmail(patient.Email, subject, body);

            return DataAccess.AppointmentData().Create(entity);
        }

        public static async Task<bool> CreateWithToken(AppointmentDTO dto)
        {
            var doctor = DataAccess.DoctorData().Get(dto.DoctorId);
            if (doctor == null)
            {
                throw new Exception("Doctor ID is invalid. Please select a valid doctor.");
            }

            var patient = DataAccess.PatientData().Get(dto.PatientId);
            if (patient == null)
            {
                throw new Exception("Patient not found.");
            }

            var conflict = DataAccess.AppointmentData().HasConflict(dto.DoctorId, dto.PatientId, dto.AppointmentDate);
            if (conflict)
            {
                throw new Exception("Appointment conflict detected. Please choose a different time.");
            }


            var entity = GetMapper().Map<Appointment>(dto);
            var created = DataAccess.AppointmentData().Create(entity);

            if (created)
            {
               // var patient = DataAccess.PatientData().Get(dto.PatientId);
               // var doctor = DataAccess.DoctorData().Get(dto.DoctorId);

                // 1. Generate PDF
                byte[] pdf = PDFHelper.GenerateAppointmentToken(patient.Name, doctor.Name, dto.AppointmentDate, entity.Id.ToString());

                // 2. Upload to Supabase
                string fileName = $"appointment_token_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string pdfUrl = await SupabaseUploader.UploadPDF(pdf, fileName);

                // 3. Send Email
                string subject = "Appointment Confirmation";
                string body = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ color: #2c3e50; text-align: center; border-bottom: 2px solid #3498db; padding-bottom: 10px; }}
        .appointment-card {{ background: #f9f9f9; border-radius: 8px; padding: 20px; margin: 20px 0; }}
        .details {{ margin: 15px 0; }}
        .button-container {{ text-align: center; margin: 25px 0; }}
        .button {{ 
            background-color: #3498db; 
            color: white; 
            padding: 12px 25px; 
            text-decoration: none; 
            border-radius: 5px; 
            font-weight: bold;
            display: inline-block;
        }}
        .footer {{ font-size: 0.9em; color: #7f8c8d; text-align: center; margin-top: 20px; }}
        .highlight {{ color: #e74c3c; font-weight: bold; }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>Your Appointment is Confirmed!</h2>
    </div>
    
    <div class='appointment-card'>
        <h3>Appointment Details</h3>
        
        <div class='details'>
            <strong>Patient:</strong> {patient.Name}<br>
            <strong>Doctor:</strong> {doctor.Name}<br>
            <strong>Date & Time:</strong> Date {dto.AppointmentDate:MMMM dd, yyyy} at {dto.AppointmentDate:h:mm tt}<br>
            <strong>Appointment ID:</strong> <span class='highlight'>{entity.Id}</span>
        </div>
        
        <div class='button-container'>
            <a href='{pdfUrl}' class='button'>Download Appointment Token</a>
        </div>
        
        <p>Please present this token when you arrive at the clinic.</p>
    </div>
    
    <p><strong>Important Reminders:</strong></p>
    <ul>
        <li>Please arrive <span class='highlight'>at least 10 minutes early</span> for your appointment</li>
        <li>Bring your ID and any relevant medical documents</li>
        <li>Contact us immediately if you need to reschedule</li>
    </ul>
    
    <div class='footer'>
        <p>Thank you for choosing our clinic!</p>
        <p>If you didn't request this appointment, please ignore this email.</p>
    </div>
</body>
</html>
";

                EmailService.SendEmail(patient.Email, subject, body);
                return true;
            }
            return false;
        }


        public static bool Update(AppointmentDTO dto)
        {
            var entity = GetMapper().Map<Appointment>(dto);
            return DataAccess.AppointmentData().Update(entity);
        }

        public static void Delete(int id)
        {
            DataAccess.AppointmentData().Delete(id);
        }

        public static AnalyticsDTO GetStats()
        {
            var all = DataAccess.AppointmentData().Get();

            var stats = new AnalyticsDTO
            {
                TotalAppointments = all.Count,
                ScheduledCount = all.Count(a => a.Status == "Scheduled"),
                CompletedCount = all.Count(a => a.Status == "Completed"),
                CancelledCount = all.Count(a => a.Status == "Cancelled"),
                AppointmentsPerDoctor = all
                    .GroupBy(a => a.Doctor?.Name ?? "Unknown")
                    .ToDictionary(g => g.Key, g => g.Count()),
                AppointmentsPerDay = all
                    .GroupBy(a => a.AppointmentDate.Date.ToShortDateString())
                    .ToDictionary(g => g.Key, g => g.Count()),
                AppointmentsPerMonth = all
                    .GroupBy(a => a.AppointmentDate.ToString("yyyy-MM"))
                    .ToDictionary(g => g.Key, g => g.Count())
            };

            return stats;
        }

    }
}
