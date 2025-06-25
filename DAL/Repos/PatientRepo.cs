using DAL.EF.Tables;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class PatientRepo : Repo, IRepo<Patient, int, bool>
    {
         public bool Create(Patient obj)
         {
             db.Patients.Add(obj);
             return db.SaveChanges() > 0;
         }

        public void Delete(int id)
        {
            var existing = Get(id);
            db.Patients.Remove(existing);
            db.SaveChanges();
        }

        public List<Patient> Get()
        {
            return db.Patients.ToList();
        }

        public Patient Get(int id)
        {
            return db.Patients.Find(id);
        }

        public bool Update(Patient obj)
        {
            var existing = Get(obj.Id);
            db.Entry(existing).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;
        }
    }
}
