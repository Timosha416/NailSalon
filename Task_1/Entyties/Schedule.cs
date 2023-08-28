using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    public class Schedule
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string Date { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan Start { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan End { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
