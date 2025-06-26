using DAL.EF.Tables;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class AppointmentRepo : Repo, IAppointmentRepo
    {
        public bool Create(Appointment obj)
        {
            var appointmentobj = db.Appointments.Add(obj);
            appointmentobj.Status = "Scheduled";
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var existing = Get(id);
            db.Appointments.Remove(existing);
           return  db.SaveChanges() > 0;
        }

        public List<Appointment> Get()
        {
            return db.Appointments.ToList();
        }



        public Appointment Get(int id)
        {
            return db.Appointments.Find(id);
        }

        public bool Update(Appointment obj)
        {
            var existing = Get(obj.Id);
            db.Entry(existing).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;
        }

        public List<Appointment> GetByDoctor(int doctorId)
        {
            return db.Appointments.Where(a => a.DoctorId == doctorId).ToList();
        }

        public List<Appointment> GetByPatient(int patientId)
        {
            return db.Appointments.Where(a => a.PatientId == patientId).ToList();
        }

        public List<Appointment> GetByDateRange(DateTime start, DateTime end)
        {
            return db.Appointments.Where(a => a.AppointmentDate >= start && a.AppointmentDate <= end).ToList();
        }

        /* public bool IsSlotAvailable(int doctorId, DateTime appointmentTime)
         {
             return !db.Appointments.Any(a => a.DoctorId == doctorId && a.AppointmentDate == appointmentTime);
         }*/

        public bool HasConflict(int doctorId, int patientId, DateTime appointmentDate)
        {
            return db.Appointments.Any(a =>
                a.AppointmentDate == appointmentDate &&
                (a.DoctorId == doctorId || a.PatientId == patientId) &&
                a.Status != "Cancelled" // optional
            );
        }

        public Appointment GetDetailed(int id)
        {
            return db.Appointments
                     .Include("Patient")
                     .Include("Doctor")
                     .FirstOrDefault(a => a.Id == id);
        }
        public bool Cancel(int appointmentId)
        {
            var appt = Get(appointmentId);
            if (appt == null) return false;

            appt.Status = "Cancelled";
            return db.SaveChanges() > 0;
        }

    }
}
