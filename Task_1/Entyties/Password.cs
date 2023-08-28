using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    public class Password
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Pass { get; set; }
    }
}
