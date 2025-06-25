using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Tables
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100), Column(TypeName = "VARCHAR")]
        public string Name { get; set; }

        [Required, EmailAddress, Column(TypeName = "VARCHAR"), MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(15), Column(TypeName = "VARCHAR")]
        public string Phone { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [MaxLength(10), Column(TypeName = "VARCHAR")]
        public string Gender { get; set; }

        [MaxLength(255), Column(TypeName = "VARCHAR")]
        public string Address { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
