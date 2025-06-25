using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAdminActivity
    {
        bool Log(AdminActivity activity);
        List<AdminActivity> GetAll();
        List<AdminActivity> GetByAdminId(int adminId);
    }
}
