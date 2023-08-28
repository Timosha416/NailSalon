using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Task_1
{
    public class Position
    {
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }
        
        public List<Employee> Employee { get; set; }
        public Position()
        {
            Employee = new List<Employee>();
        }
    }
}
