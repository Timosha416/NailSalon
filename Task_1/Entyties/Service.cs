using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Task_1
{
    public class Service
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "money")]
        public double Price { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan Lenght { get; set; }
        public List<Order> Order { get; set; }
        public Service()
        {
            Order = new List<Order>();
        }
    }
}
