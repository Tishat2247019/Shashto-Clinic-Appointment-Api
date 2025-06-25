using DAL.EF.Tables;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class AdminActivityRepo : Repo, IAdminActivity
    {
        public bool Log(AdminActivity activity)
        {
            db.AdminActivities.Add(activity);
            return db.SaveChanges() > 0;
        }

        public List<AdminActivity> GetAll()
        {
            return db.AdminActivities.ToList();
        }

        public List<AdminActivity> GetByAdminId(int adminId)
        {
            return db.AdminActivities
                     .Where(a => a.AdminId == adminId)
                     .OrderByDescending(a => a.Timestamp)
                     .ToList();
        }
    }
}
