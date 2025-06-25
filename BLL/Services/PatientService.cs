using BLL.DTOs;
using DAL.EF.Tables;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL.Services
{
    public class PatientService
    {
        private static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Patient, PatientDTO>();
                cfg.CreateMap<PatientDTO, Patient>();
                cfg.CreateMap<PatientRegistrationDTO, Patient>();
            });
            return new Mapper(config);
        }

        public static List<PatientDTO> Get()
        {
            var data = DataAccess.PatientData().Get();
            return GetMapper().Map<List<PatientDTO>>(data);
        }

        public static PatientDTO Get(int id)
        {
            var data = DataAccess.PatientData().Get(id);
            return GetMapper().Map<PatientDTO>(data);
        }

        public static bool Create(PatientDTO dto)
        {
            var entity = GetMapper().Map<Patient>(dto);
            return DataAccess.PatientData().Create(entity);
        }

        public static bool Update(PatientDTO dto)
        {
            var entity = GetMapper().Map<Patient>(dto);
            return DataAccess.PatientData().Update(entity);
        }

        public static bool Delete(int id)
        {
           return DataAccess.PatientData().Delete(id);
        }
        public static bool Register(PatientRegistrationDTO regDto)
        {
            var patient = GetMapper().Map<Patient>(regDto);
            var success = DataAccess.PatientData().Create(patient);
            if (success)
            {
                var createdPatient = DataAccess.PatientData()
                    .Get().LastOrDefault(p => p.Email == regDto.Email);
                if (createdPatient != null)
                {
                    var login = new Login
                    {
                        Email = regDto.Email,
                        Password = regDto.Password,
                        UserType = "Patient",
                        UserId = createdPatient.Id
                    };
                    DataAccess.LoginData().Create(login);
                    return true;
                }
            }
            return false;
        }
    }
}
