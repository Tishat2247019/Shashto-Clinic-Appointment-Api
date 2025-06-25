using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class DoctorSearchDTO
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; }
    }
}
