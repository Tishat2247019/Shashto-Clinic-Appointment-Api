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
    }
}
