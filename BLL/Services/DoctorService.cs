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
    public class DoctorService
    {
        private static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Doctor, DoctorDTO>();
                cfg.CreateMap<DoctorDTO, Doctor>();
                cfg.CreateMap<DoctorRegistrationDTO, Doctor>();

            });
            return new Mapper(config);
        }

        public static List<DoctorDTO> Get()
        {
            var data = DataAccess.DoctorData().Get();
            return GetMapper().Map<List<DoctorDTO>>(data);
        }

        public static DoctorDTO Get(int id)
        {
            var data = DataAccess.DoctorData().Get(id);
            return GetMapper().Map<DoctorDTO>(data);
        }

        public static bool Create(DoctorDTO dto)
        {
            var doctor = GetMapper().Map<Doctor>(dto);
            return DataAccess.DoctorData().Create(doctor);
        }

        public static bool Register(DoctorRegistrationDTO dto)
        {
            var existingDoctor = DataAccess.DoctorData()
                                 .Get().FirstOrDefault(p => p.Email == dto.Email);
            if (existingDoctor != null)
                return false;
            var doctor = GetMapper().Map<Doctor>(dto);
            var success = DataAccess.DoctorData().Create(doctor);
            if (success)
            {
                var createdDoctor = DataAccess.DoctorData()
                    .Get().LastOrDefault(p => p.Email == dto.Email);
                if (createdDoctor != null)
                {
                    var login = new Login
                    {
                        Email = dto.Email,
                        Password = dto.Password,
                        UserType = "Doctor",
                        UserId = createdDoctor.Id
                    };
                    DataAccess.LoginData().Create(login);
                    return true;
                }
            }
            return false;
        }

        public static bool Update(DoctorDTO dto)
        {
            var doctor = GetMapper().Map<Doctor>(dto);
            return DataAccess.DoctorData().Update(doctor);
        }

        public static void Delete(int id)
        {
            DataAccess.DoctorData().Delete(id);
        }

        public static List<DoctorDTO> Search(DoctorSearchDTO filter)
        {
            var doctors = DataAccess.DoctorData().Get();

            if (!string.IsNullOrWhiteSpace(filter.Name))
                doctors = doctors.Where(d => d.Name.ToLower().Contains(filter.Name.ToLower())).ToList();

            if (!string.IsNullOrWhiteSpace(filter.Department))
                doctors = doctors.Where(d => d.Department == filter.Department).ToList();

            if (!string.IsNullOrWhiteSpace(filter.Specialty))
                doctors = doctors.Where(d => d.Speciality == filter.Specialty).ToList();

            if (!string.IsNullOrWhiteSpace(filter.Email))
                doctors = doctors.Where(d => d.Email == filter.Email).ToList();

            return GetMapper().Map<List<DoctorDTO>>(doctors);
        }

    }
}
