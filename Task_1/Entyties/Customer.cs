using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Task_1
{
    public class Customer
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(13)]
        public string Phone { get; set; }
        public List<Order> Order { get; set; }
        public Customer()
        {
            Order = new List<Order>();
        }
    }
}
