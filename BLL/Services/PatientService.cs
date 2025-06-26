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
            var existingPatient = DataAccess.PatientData()
                                  .Get().FirstOrDefault(p => p.Email == regDto.Email);
            if (existingPatient != null)
                return false; 

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

        public static PatientEngagementDTO GetEngagementStats()
        {
            var patients = DataAccess.PatientData().Get();
            var appointments = DataAccess.AppointmentData().Get();

            var stats = new PatientEngagementDTO
            {
                TotalPatients = patients.Count,

               /* NewPatientsPerMonth = patients
                    .GroupBy(p => p.CreatedAt.ToString("yyyy-MM"))
                    .OrderByDescending(g => g.Key)
                    .Take(6)
                    .ToDictionary(g => g.Key, g => g.Count()),
               */

                MostFrequentPatients = appointments
                    .GroupBy(a => a.PatientId)
                    .Select(g => new
                    {
                        PatientId = g.Key,
                        Count = g.Count()
                    })
                    .OrderByDescending(x => x.Count)
                    .Take(5)
                    .Select(x =>
                    {
                        var patient = patients.FirstOrDefault(p => p.Id == x.PatientId);
                        return new FrequentPatientDTO
                        {
                            PatientId = x.PatientId,
                            PatientName = patient?.Name ?? "Unknown",
                            AppointmentCount = x.Count
                        };
                    })
                    .ToList()
            };

            return stats;
        }
    }
}
