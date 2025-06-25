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
    public class AdminActivityService
    {
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AdminActivity, AdminActivityDTO>().ReverseMap();
            });
            return new Mapper(config);
        }

        public static bool LogActivity(AdminActivityDTO dto)
        {
            dto.Timestamp = DateTime.Now;
            var entity = GetMapper().Map<AdminActivity>(dto);
            return DataAccess.AdminActivityData().Log(entity);
        }

        public static List<AdminActivityDTO> GetAllLog()
        {
            var data = DataAccess.AdminActivityData().GetAll();
            return GetMapper().Map<List<AdminActivityDTO>>(data);
        }
        public static List<AdminActivityDTO> GetLogsByAdmin(int adminId)
        {
            var data = DataAccess.AdminActivityData().GetByAdminId(adminId);
            return GetMapper().Map<List<AdminActivityDTO>>(data);
        }
        public static byte[] GenerateActivityLogPdf(string generatedBy)
        {
            var logs = DataAccess.AdminActivityData().GetAll();
            return AdminLogPDFGenerator.Generate(logs, generatedBy);
        }

    }
}
