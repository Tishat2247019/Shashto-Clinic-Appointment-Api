using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAppointmentRepo : IRepo<Appointment, int, bool>
    {
        List<Appointment> GetByDoctor(int doctorId);
        List<Appointment> GetByPatient(int patientId);
        List<Appointment> GetByDateRange(DateTime start, DateTime end);

        //bool IsSlotAvailable(int doctorId, DateTime appointmentTime);
        bool HasConflict(int doctorId, int patientId, DateTime appointmentDate);
        Appointment GetDetailed(int id);
        bool Cancel(int appoiontmentId);
    }
}
