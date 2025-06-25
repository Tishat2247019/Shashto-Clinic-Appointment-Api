using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AdminActivityDTO
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
