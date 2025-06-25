using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Tables
{
    public class Token
    {
        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "VARCHAR"), MaxLength(200)]
        public string Key { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? ExpiredAt { get; set; }

        public int UserId { get; set; }     // ID of patient or doctor

        [Required, MaxLength(20)]
        public string UserType { get; set; } // "Patient" or "Doctor

        [ForeignKey("Login")]
        public int LoginId { get; set; }
        public virtual Login Login { get; set; }
    }
}
