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

        public static bool Update(DoctorDTO dto)
        {
            var doctor = GetMapper().Map<Doctor>(dto);
            return DataAccess.DoctorData().Update(doctor);
        }

        public static void Delete(int id)
        {
            DataAccess.DoctorData().Delete(id);
        }
    }
}
