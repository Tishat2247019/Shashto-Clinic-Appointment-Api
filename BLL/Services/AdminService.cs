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
    public class AdminService
    {
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Admin, AdminDTO>();
                cfg.CreateMap<AdminDTO, Admin>();
            });
            return new Mapper(config);
        }

        public static List<AdminDTO> Get()
        {
            var data = DataAccess.AdminData().Get();
            return GetMapper().Map<List<AdminDTO>>(data);
        }

        public static AdminDTO Get(int id)
        {
            var data = DataAccess.AdminData().Get(id);
            return GetMapper().Map<AdminDTO>(data);
        }

        public static bool Create(AdminDTO dto)
        {
            var entity = GetMapper().Map<Admin>(dto);
          //  return DataAccess.AdminData().Create(entity);
            var success = DataAccess.AdminData().Create(entity);
            if (success)
            {
                var createdAdmin = DataAccess.AdminData()
                    .Get().LastOrDefault(p => p.Email == dto.Email);
                if (createdAdmin != null)
                {
                    var login = new Login
                    {
                        Email = dto.Email,
                        Password = dto.Password,
                        UserType = "Admin",
                        UserId = createdAdmin.Id
                    };
                    DataAccess.LoginData().Create(login);
                    return true;
                }
            }
            return false;
        }

        public static bool Update(AdminDTO dto)
        {
            var entity = GetMapper().Map<Admin>(dto);
            return DataAccess.AdminData().Update(entity);
        }

        public static bool Delete(int id)
        {
          var result =  DataAccess.AdminData().Delete(id);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
