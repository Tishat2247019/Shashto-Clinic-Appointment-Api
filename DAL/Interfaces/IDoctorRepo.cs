using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDoctorRepo : IRepo<Doctor, int, bool>
    {
        List<Doctor> GetBySpeciality(string speciality);
    }
}
