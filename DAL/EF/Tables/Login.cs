using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Tables
{
    public class Login
    {
        [Key]
        public int Id { get; set; }

        [Required, EmailAddress, MaxLength(100), Column(TypeName = "VARCHAR")]
        public string Email { get; set; }

        [Required, MaxLength(100), Column(TypeName = "VARCHAR")]
        public string Password { get; set; }

        [Required, MaxLength(50), Column(TypeName = "VARCHAR")]
        public string UserType { get; set; }  // "Patient", "Doctor", etc.

        public int UserId { get; set; }  // ID from Patient or Doctor table

        public virtual ICollection<Token> Tokens { get; set; }
    }
}
