using DAL.EF.Tables;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccess
    {
        public static IRepo<Patient, int, bool> PatientData()
        {
            return new PatientRepo();
        }
        public static IRepo<Doctor, int, bool> DoctorData()
        {
            return new DoctorRepo();
        }
        public static IAppointmentRepo AppointmentData()
        {
            return new AppointmentRepo();
        }
        public static IRepo<Login, string, Login> LoginData()
        {
            return new LoginRepo();
        }
        public static IRepo<Token, string, Token> TokenData()
        {
            return new TokenRepo();
        }
        public static IAuth AuthData()
        {
            return new LoginRepo();
        }
        public static IRepo<Admin, int, bool> AdminData()
        {
            return new AdminRepo();
        }

        public static IAdminActivity AdminActivityData()
        {
            return new AdminActivityRepo();
        }
    }
}
