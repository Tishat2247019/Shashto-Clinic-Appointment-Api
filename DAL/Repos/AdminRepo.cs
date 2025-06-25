using DAL.EF.Tables;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class AdminRepo : Repo, IRepo<Admin, int, bool>
    {
        public bool Create(Admin admin)
        {
            db.Admins.Add(admin);
            return db.SaveChanges() > 0;
        }

        public bool Update(Admin admin)
        {
            var exAdmin = Get(admin.Id);
            if (exAdmin == null) return false;
            db.Entry(exAdmin).CurrentValues.SetValues(admin);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var admin = Get(id);
            if (admin == null) return false;
            db.Admins.Remove(admin);
            return db.SaveChanges() > 0;
        }

        public Admin Get(int id)
        {
            return db.Admins.Find(id);
        }

        public List<Admin> Get()
        {
            return db.Admins.ToList();
        }
    }
}
