using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utils
{
    public static class AdminLogger
    {
        public static void Log(int adminId, string action, string details)
        {
            var log = new AdminActivityDTO
            {
                AdminId = adminId,
                Action = action,
                Details = details
            };
            AdminActivityService.LogActivity(log);
        }
    }
}
