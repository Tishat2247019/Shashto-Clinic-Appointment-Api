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

        public static void Delete(int id)
        {
            DataAccess.PatientData().Delete(id);
        }
    }
}
