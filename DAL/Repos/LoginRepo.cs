using DAL.EF.Tables;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class LoginRepo : Repo, IRepo<Login, string, Login>, IAuth
    {
        public Login Authenticate(string email, string pass)
        {
            return db.Logins.FirstOrDefault(l => l.Email == email && l.Password == pass);
        }

        public Login Create(Login obj)
        {
            db.Logins.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public void Delete(string email)
        {
            var login = Get(email);
            if (login != null)
            {
                db.Logins.Remove(login);
                db.SaveChanges();
            }
        }

        public List<Login> Get()
        {
            return db.Logins.ToList();
        }

        public Login Get(string email)
        {
            return db.Logins.FirstOrDefault(l => l.Email == email);
        }

        public Login Update(Login obj)
        {
            var existing = Get(obj.Email);
            if (existing != null)
            {
                db.Entry(existing).CurrentValues.SetValues(obj);
                db.SaveChanges();
                return existing;
            }
            return null;
        }
    }
}
