using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Reason { get; set; }

       public string Status { get; set; }  

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public string PatientName { get; set; }

        public string DoctorName { get; set; }
    }
}
