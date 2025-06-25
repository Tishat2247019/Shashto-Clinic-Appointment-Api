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

        public bool Delete(int id)
        {
            var existing = Get(id);
            if(existing == null)
               return false;
            db.Patients.Remove(existing);
               return db.SaveChanges() > 0 ;
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
            if (existing == null) return false;

            if (!string.IsNullOrWhiteSpace(obj.Name)) existing.Name = obj.Name;
            if (!string.IsNullOrWhiteSpace(obj.Email)) existing.Email = obj.Email;
            if (!string.IsNullOrWhiteSpace(obj.Phone)) existing.Phone = obj.Phone;
            if (obj.DOB != default) existing.DOB = obj.DOB;
            if (!string.IsNullOrWhiteSpace(obj.Gender)) existing.Gender = obj.Gender;
            if (!string.IsNullOrWhiteSpace(obj.Address)) existing.Address = obj.Address;

            return db.SaveChanges() > 0;
        }

    }
}
