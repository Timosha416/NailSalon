using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Task_1
{
    public class Employee
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(13)]
        public string Phone { get; set; }
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
        public List<Order> Order { get; set; }
        public List<Schedule> Schedule { get; set; }
        public Employee()
        {
            Order = new List<Order>();
            Schedule = new List<Schedule>();    
        }
    }
}
