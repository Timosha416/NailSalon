using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace Task_1
{
    public class OrderVM : INotifyPropertyChanged
    {
        public Order ModelOrder { get; set; }

        public int Id
        {
            get { return ModelOrder.Id; }
            set
            {
                ModelOrder.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public int EmployeeId
        {
            get { return ModelOrder.EmployeeId; }
            set
            {
                ModelOrder.EmployeeId = value;
                OnPropertyChanged(nameof(EmployeeId));
            }
        }
        public int CustomerId
        {
            get { return ModelOrder.CustomerId; }
            set
            {
                ModelOrder.CustomerId = value;
                OnPropertyChanged(nameof(CustomerId));
            }
        }
        public int ServiceId
        {
            get { return ModelOrder.ServiceId; }
            set
            {
                ModelOrder.ServiceId = value;
                OnPropertyChanged(nameof(ServiceId));
            }
        }
        public DateTime Date
        {
            get { return ModelOrder.Date; }
            set
            {
                ModelOrder.Date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        public TimeSpan Start
        {
            get { return ModelOrder.Start; }
            set
            {
                ModelOrder.Start = value;
                OnPropertyChanged(nameof(Start));
            }
        }
       
        public Employee Employee
        {
            get { return ModelOrder.Employee; }
            set
            {
                ModelOrder.Employee = value;
                OnPropertyChanged(nameof(Employee));
            }
        }
        public Customer Customer
        {
            get { return ModelOrder.Customer; }
            set
            {
                ModelOrder.Customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }
        public Service Service
        {
            get { return ModelOrder.Service; }
            set
            {
                ModelOrder.Service = value;
                OnPropertyChanged(nameof(Service));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

