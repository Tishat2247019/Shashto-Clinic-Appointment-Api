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
    public class AuthService
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Token, TokenDTO>();
            });
            return new Mapper(config);
        }

        public static TokenDTO Auth(string email, string pass)
        {
            var user = DataAccess.AuthData().Authenticate(email, pass);
            if (user != null)
            {
                Token tk = new Token
                {
                    Key = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    ExpiredAt = null,
                    UserId = user.UserId,
                    UserType = user.UserType,
                    LoginId = user.Id
                };

                var token = DataAccess.TokenData().Create(tk);
                var dto = GetMapper().Map<TokenDTO>(token);
                
                // dto.UserType = user.UserType;

                return dto;
            }
            return null;
        }


        public static bool IsTokenValid(string key)
        {
            var token = DataAccess.TokenData().Get(key);
            if (token != null && token.ExpiredAt == null)
            {
                return true;
            }
            return false;
        }

        public static TokenDTO Logout(string key)
        {
            var token = DataAccess.TokenData().Get(key);
            if (token == null) return null;

            // Optional: expire 2 mins before current time (to simulate logout logic)
            token.ExpiredAt = DateTime.Now.AddMinutes(-2);
            var updatedToken = DataAccess.TokenData().Update(token);
            return GetMapper().Map<TokenDTO>(updatedToken);
        }

        public static TokenDTO GetToken(string key)
        {
            var tokenEntity = DataAccess.TokenData().Get(key);
            if (tokenEntity == null) return null;

            var dto = GetMapper().Map<TokenDTO>(tokenEntity); 
           // dto.UserType = tokenEntity.Login.UserType; 
            return dto;
        }

    }
}
