using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Tables
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(15)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Speciality { get; set; }

        [MaxLength(100)]
        public string Department { get; set; }

        // Navigation Property
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
