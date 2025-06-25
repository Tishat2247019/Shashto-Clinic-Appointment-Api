using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return DataAccess.AppointmentData().Create(entity);
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
    }
}
