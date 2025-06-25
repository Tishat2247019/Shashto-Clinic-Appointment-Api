using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AnalyticsDTO
    {
        public int TotalAppointments { get; set; }
        public int ScheduledCount { get; set; }
        public int CompletedCount { get; set; }
        public int CancelledCount { get; set; }

        public Dictionary<string, int> AppointmentsPerDoctor { get; set; }
        public Dictionary<string, int> AppointmentsPerDay { get; set; }
        public Dictionary<string, int> AppointmentsPerMonth { get; set; }
    }
}
