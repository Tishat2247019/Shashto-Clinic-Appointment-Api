using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class PatientRegistrationDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
        public string Password { get; set; } 
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
    }
}
