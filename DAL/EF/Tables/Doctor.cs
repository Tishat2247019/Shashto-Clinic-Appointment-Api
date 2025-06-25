using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Tables
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100), Column(TypeName = "VARCHAR")]
        public string Name { get; set; }

        [Required, EmailAddress, Column(TypeName = "VARCHAR"), MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(15), Column(TypeName = "VARCHAR")]
        public string Phone { get; set; }

        [MaxLength(100), Column(TypeName = "VARCHAR")]
        public string Speciality { get; set; }

        [MaxLength(100), Column(TypeName = "VARCHAR")]
        public string Department { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
