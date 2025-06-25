using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Tables
{
    public class AdminActivity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AdminId { get; set; }

        [Required, MaxLength(100)]
        public string Action { get; set; }

        [MaxLength(255)]
        public string Details { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [ForeignKey("AdminId")]
        public virtual Admin Admin { get; set; }
    }
}
