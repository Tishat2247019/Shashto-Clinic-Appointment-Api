using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class PatientEngagementDTO
    {
        public int TotalPatients { get; set; }
       // public Dictionary<string, int> NewPatientsPerMonth { get; set; }
        public List<FrequentPatientDTO> MostFrequentPatients { get; set; }
    }

    public class FrequentPatientDTO
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int AppointmentCount { get; set; }
    }
}
